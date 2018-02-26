using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApplication2.Models.Database
{
    public partial class ReviewDatabase : AbstractDatabase
    {
        private ReviewDatabase() { }

        public static ReviewDatabase getInstance()
        {
            if(instance == null)
            {
                instance = new ReviewDatabase();
            }
            return instance;
        }

        /// <summary>
        /// Gets a long string review based on the string id of the company name
        /// </summary>
        /// <param name="companyname">The id of the short url</param>
        /// <throws type="ArgumentException">Throws an argument exception if the short url id does not refer to anything in the database</throws>
        /// <returns>The review(s) from the given company name refers to</returns>
        public string getReviews(string companyname)
        {
            string query = @"SELECT * FROM " + dbname + ".companyReviews "
                + "WHERE id=" + companyname + ";";

            if(openConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                if(reader.Read() == true)
				{
					string originalUrl = reader.GetString("original");
					reader.Close();
					closeConnection();
					return originalUrl;
				}
                else
                {
                    //Throw an exception indicating no result was found
                    throw new ArgumentException("No company name in the database matches that name.");
                }
            }
            else
            {
                throw new Exception("Could not connect to database.");
            }
        }

        /// <summary>
        /// Saves the review to the database to be accessed later via the companyname.
        /// </summary>
        /// <param name="review">The review to be saved</param>
        /// <returns>The id of the url</returns>
        public void saveReview(string review)
        {
            string query = @"INSERT INTO " + dbname + ".companyReviews(original) "
                + @"VALUES('" + review + @"');";

            if(openConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                command.CommandText = "SELECT * FROM " + dbname + ".companyReviews WHERE id = LAST_INSERT_ID();";

                MySqlDataReader reader = command.ExecuteReader();

                if(reader.Read() == true)
                {
                    closeConnection();
                }
                else
                {
                    reader.Close();
                    closeConnection();
                    throw new Exception("Error: LAST_INSERT_ID() did not work as intended.");
                }
            }
            else
            {
                throw new Exception("Could not connect to database");
            }
        }
    }

    public partial class ReviewDatabase : AbstractDatabase
    {
        private static ReviewDatabase instance = null;

        private const String dbname = "reviewdb";
        public override String databaseName { get; } = dbname;

        protected override Table[] tables { get; } =
        {
            // This represents the database schema
            new Table
            (
                dbname,
                "reviews",
                new Column[]
                {
                    new Column
                    (
                        "companyName", "VARCHAR(500)",
                        new string[]
                        {
                            "NOT NULL",
                            "UNIQUE",
                            "AUTO_INCREMENT"
                        }, true
                    ),
                    new Column
                    (
                        "original", "VARCHAR(300)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    )
                }
            )
        };
    }
}