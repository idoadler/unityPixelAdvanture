using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;  

public class ReadTiles {
	static public MapsData LoadMap(string mapsFolder)
	{
		MapsData mapsData = new MapsData();
		mapsData.fileName = mapsFolder;

		TextAsset[] mapsText = Resources.LoadAll<TextAsset>(mapsFolder);

		string[] parameters = mapsText[0].text.Split(' ');
		mapsData.emptyChar = parameters[0].ToCharArray()[0];
		mapsData.blockChar = parameters[1].ToCharArray()[0];
		mapsData.playerChar = parameters[2].ToCharArray()[0];
		mapsData.exitChar = parameters[3].ToCharArray()[0];

		// Handle any problems that might arise when reading the text
		for(int file = 1; file < mapsText.Length; file++)
		{
			MapData mapData = new MapData();
			string[] lines = mapsText[file].text.Split('\n');

			string[] firstRow = lines[0].Split(' ');
			for(int i = 0; i < firstRow.Length && firstRow[i].Length > 0; i++)
			{
				List<char> newLine = new List<char>();
				newLine.Add(firstRow[i].ToCharArray()[0]);
				mapData.map.Add(newLine);
			}


			// While there's lines left in the text file, do this:
			for(int line = 1; line < lines.Length; line++)
			{
				if (lines[line].Contains(mapsData.playerChar.ToString()))
				{
					mapData.startPos = new Vector2(lines[line].IndexOf(mapsData.playerChar)/2, mapData.map[lines[line].IndexOf(mapsData.playerChar)/2].Count);
					lines[line] = lines[line].Replace(mapsData.playerChar,mapsData.emptyChar);
				}

				// Do whatever you need to do with the text line, it's a string now
				// In this example, I split it into arguments based on comma
				// deliniators, then send that array to DoStuff()
				string[] entries = lines[line].Split(' ');
				for(int i = 0; i < entries.Length  && entries[i].Length > 0; i++)
				{
					mapData.map[i].Add(entries[i].ToCharArray()[0]);
				}
			}

			mapsData.maps.Add(mapData);
		}

		return mapsData;
	}
}
