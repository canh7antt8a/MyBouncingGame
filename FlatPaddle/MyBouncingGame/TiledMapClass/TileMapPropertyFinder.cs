﻿using System;
using CocosSharp;
using System.Collections.Generic;

namespace MyBouncingGame.TiledMapClass
{
	public class TileMapPropertyFinder
	{
		CCTileMap tileMap;
		public TileMapPropertyFinder (CCTileMap tileMap)
		{
			this.tileMap = tileMap;
		}


		public IEnumerable<PropertyLocation> GetPropertyLocations()
		{
			// Width and Height are equal so we can use either
			//得到每一个小瓦片的边长
			int tileDimensionWidth = (int)tileMap.TileTexelSize.Width;
			int tileDimensionHeight = (int)tileMap.TileTexelSize.Height;

			// Find out how many rows and columns are in our tile map
			int numberOfColumns = (int)tileMap.MapDimensions.Size.Width;
			int numberOfRows = (int)tileMap.MapDimensions.Size.Height;

			// Tile maps can have multiple layers, so let's loop through all of them:
			foreach (CCTileMapLayer layer in tileMap.TileLayersContainer.Children)
			{
				// Loop through the columns and rows to find all tiles
				for (int column = 0; column < numberOfColumns; column++)
				{
					// We're going to add tileDimension / 2 to get the position
					// of the center of the tile - this will help us in 
					// positioning entities, and will eliminate the possibility
					// of floating point error when calculating the nearest tile:
					int worldX = tileDimensionWidth * column + tileDimensionWidth / 2;
					for (int row = 0; row < numberOfRows; row++)
					{
						// See above on why we add tileDimension / 2
						int worldY = tileDimensionHeight * row + tileDimensionHeight / 2;
						//得到每个小瓦片的中心位置(worldX,worldY)

						//得到离那个中心点最近的瓦片的位置信息（坐标）
						CCTileMapCoordinates tileAtXy = layer.ClosestTileCoordAtNodePosition (new CCPoint (worldX, worldY));

						CCTileGidAndFlags info = layer.TileGIDAndFlags (tileAtXy.Column, tileAtXy.Row);

						if (info != null)
						{
							Dictionary<string, string> properties = null;

							try
							{
								properties = tileMap.TilePropertiesForGID (info.Gid);
							}
							catch
							{
								// CocosSharp crashed here...but this may be fixed in the current version
							}

							if (properties != null)
							{
								//yield return 是为了遍历时返回每个瓦片的信息
								yield return new PropertyLocation {
									WorldX = worldX,
									WorldY = worldY,
									Properties = properties,
									Layer = layer,
									TileCoordinates = tileAtXy
								};
							}
						}
					}
				}
			}


		}
	}
}
