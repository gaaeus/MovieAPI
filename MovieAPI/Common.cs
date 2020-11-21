using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace MovieAPI
{
    public class Common
    {
        const string DATA_PATH = "Assets";
        const string METADATA_FILEPATH = "metadata.csv";
        const string STATS_FILEPATH = "stats.csv";

        /// <summary>
        /// Retrieves the contents of the metadata
        /// </summary>
        /// <returns>List with the metadata's contents</returns>
        public async Task<List<Metadata>> GetMetadata()
        {
            var metadataList = new List<Metadata>();
            
            try
            {
                var metadataContents = await ReadFileContents(Path.Combine(DATA_PATH, METADATA_FILEPATH));

                metadataContents = metadataContents.Skip(1).ToArray(); // We do not require the first line

                foreach (var metadataLine in metadataContents)
                {
                    var parser = new TextFieldParser(new StringReader(metadataLine));
                    parser.HasFieldsEnclosedInQuotes = true;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        var metadataColumns = parser.ReadFields();
                        var metadataObject = new Metadata
                        {
                            Id = Convert.ToInt32(metadataColumns[0]),
                            MovieId = Convert.ToInt32(metadataColumns[1]),
                            Title = metadataColumns[2].Replace("\"", "'"),
                            Language = metadataColumns[3],
                            Duration = metadataColumns[4],
                            ReleaseYear = Convert.ToInt32(metadataColumns[5])
                        };

                        metadataList.Add(metadataObject);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return metadataList;
        }

        /// <summary>
        /// Retrieves the contents of the stats
        /// </summary>
        /// <returns>List with the stats' contents</returns>
        public async Task<List<Stats>> GetStats()
        {
            var statsList = new List<Stats>();

            try
            {
                var statsContents = await ReadFileContents(Path.Combine(DATA_PATH, STATS_FILEPATH));
                statsContents = statsContents.Skip(1).ToArray(); // We do not require the first line

                foreach (var statsLine in statsContents)
                {
                    var parser = new TextFieldParser(new StringReader(statsLine));
                    parser.HasFieldsEnclosedInQuotes = true;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        var statsColumns = parser.ReadFields();
                        var statsObject = new Stats
                        {
                            MovieId = Convert.ToInt32(statsColumns[0]),
                            WatchDurationMs = Convert.ToInt32(statsColumns[1])
                        };

                        statsList.Add(statsObject);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return statsList;
        }

        /// <summary>
        /// Asynchronously reads the contents of a file
        /// </summary>
        /// <param name="filename">The name/path of the file</param>
        /// <returns>An string array with the file's contents</returns>
        private async Task<string[]> ReadFileContents(string filename)
        {
            var fileContents = File.ReadAllLinesAsync(filename);

            return await fileContents;
        }
    }
}
