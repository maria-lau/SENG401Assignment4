using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;

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
        /// Gets a ReviewInfo Object based on the string id of the company name
        /// </summary>
        /// <param name="companyName">The id of the short url</param>
        /// <throws type="ArgumentException">Throws an argument exception if the short url id does not refer to anything in the database</throws>
        /// <returns>The review(s) from the given company name refers to</returns>
        public JObject getReviews(string companyName)
        {
            string query = @"SELECT * FROM " + dbname + ".companyReviews "
                + "WHERE companyName='" + companyName + "';";

            if(openConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                JObject reviews = null, temp = null;

                if(reader.HasRows)
				{
                    while (reader.Read())
                    {
                        temp = JObject.Parse(reader.GetString(2));
                        if (reviews == null)
                        {
                            reviews = temp;
                        }
                        else
                        {
                            reviews.Merge(temp);
                        }
                    }

					reader.Close();
					closeConnection();
                    return reviews;
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
        /// Receives a ReviewInfo object as a parameter and
        /// Saves the review to the database with the company name as the key
        /// </summary>
        /// <param name="review">The review to be saved</param>
        /// <returns>returns a JSON object indicating if it was successful or not</returns>
        public JObject addReview(ReviewInfo review)
        {
            string jsonstring = "{'companyName':'"+ review.companyName + "','username':'" + review.username + "','review':'" + review.review 
                                + "','stars':" + review.stars + ",'timestamp':" + review.timestamp + "}";
            JObject newReview = new JObject(JObject.Parse(jsonstring));
            
            string query = "INSERT INTO " + dbname + ".companyReviews(companyName, reviews) " + "VALUES ('" + review.companyName + "','" + newReview + "');";

            if(openConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                command.CommandText = "SELECT * FROM " + dbname + ".companyReviews WHERE companyName = LAST_INSERT_ID();";

                MySqlDataReader reader = command.ExecuteReader();

                if(reader.Read() == true)
                {
                    reader.Close();
                    closeConnection();
                    return JObject.Parse("{\"response\":\"success\"}");
                }
                else
                {
                    reader.Close();
                    closeConnection();
                    return JObject.Parse("{\"response\":\"failure\"}");
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
                "companyReviews",
                new Column[]
                {
                    new Column
                    (
                        "id", "INT(64)",
                        new string[]
                        {
                            "NOT NULL",
                            "UNIQUE",
                            "AUTO_INCREMENT"
                        }, true
                    ),
                    new Column
                    (
                        "companyName", "VARCHAR(100)",
                        new string[]
                        {
                            "NOT NULL",
                        },false 
                    ),
                    new Column
                    (
                        "reviews", "JSON",
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