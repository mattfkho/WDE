using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WDE.Clients;
using WDE.Models;

namespace WDE.Controllers
{
    /// <summary>
    /// Thte Answers
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private string _apiBaseUrl = "http://dev-wooliesx-recruitment.azurewebsites.net";

        private Guid _apiTopken = new Guid("92f7aa8b-e4ae-4382-8140-6ffdbb37e8bf");

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("User")]
        public ActionResult Ex1GetUser()
        {
            var user = new User
            {
                //Name = "test",
                //Token = "1234-455662-22233333-3333"
                Name = "Matthew Ho",
                Token = _apiTopken.ToString()
            };

            return Ok(user);
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Sort")]
        public ActionResult Ex2GetSortedProducts(string sortOption = "")
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(_apiBaseUrl) };
            var apiClient = new ApiClient(httpClient);

            List<Product> sortedProducts = null;

            try
            {
                switch (sortOption.ToUpper())
                {
                    case "LOW":
                        sortedProducts = GetProducts(apiClient, _apiTopken).OrderBy(x => x.Price).ToList();
                        break;

                    case "HIGH":
                        sortedProducts = GetProducts(apiClient, _apiTopken).OrderByDescending(x => x.Price).ToList();
                        break;

                    case "ASCENDING":
                        sortedProducts = GetProducts(apiClient, _apiTopken).OrderBy(x => x.Name).ToList();
                        break;

                    case "DESCENDING":
                        sortedProducts = GetProducts(apiClient, _apiTopken).OrderByDescending(x => x.Name).ToList();
                        break;

                    case "RECOMMENDED":
                        sortedProducts = RecommendedProductsByShopperHistory(apiClient, _apiTopken).ToList();
                        break;

                    default:
                        sortedProducts = GetProducts(apiClient, _apiTopken).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("\n## error ## {0}\n", ex.Message.ToString()));
            }

            return Ok(sortedProducts);
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <param name="apiClient">The API client.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        private List<Product> GetProducts(ApiClient apiClient, Guid token)
        {
            var products = apiClient.ApiResourceProductsGetAsync(token).Result;
            return products.ToList();
        }

        /// <summary>
        /// Recommendeds the products by shopper history.
        /// </summary>
        /// <param name="apiClient">The API client.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        private List<Product> RecommendedProductsByShopperHistory(ApiClient apiClient, Guid token)
        {
            List<Product> sortedProducts;

            var shopperHistory = apiClient.ApiResourceShopperHistoryGetAsync(token).Result;
            List<Product> products2 = new List<Product>();
            foreach (var h in shopperHistory)
            {
                products2.AddRange(h.Products);
            }

            var popularProducts = products2.GroupBy(x => x.Name).Select(g => new Product
            {
                Name = g.Key,
                Price = g.First().Price,
                Quantity = g.Sum(x => x.Quantity)
            }).OrderByDescending(x => x.Quantity).ToList(); // assuming "popular" means higher "Quantity"
                                                            //}).OrderByDescending(x => x.Quantity * x.Price).ToList();

            //sortedProducts = popularProducts.Select(x => { return new Product { Name = x.Name, Price = x.Price, Quantity = 0 }; }).ToList();
            sortedProducts = popularProducts.ToList();
            return sortedProducts;
        }

        /// <summary>
        /// Gets the trolley total.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("TrolleyTotal")]
        public ActionResult Ex3GetTrolleyTotal()
        {
            string result = null;

            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var trolleyRequest = JsonConvert.DeserializeObject<Trolley>(reader.ReadToEnd());
                    Console.WriteLine(string.Format("\n## request ## {0}\n", JsonConvert.SerializeObject(trolleyRequest)));

                    if (trolleyRequest != null)
                    {
                        var httpClient = new HttpClient { BaseAddress = new Uri(_apiBaseUrl) };
                        var apiClient = new ApiClient(httpClient);

                        result = apiClient.ApiResourceTrolleyCalculatorPostAsync(_apiTopken, trolleyRequest).Result.ToString();
                        Console.WriteLine(string.Format("\n## response ## {0}\n", result));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("\n## error ## {0}\n", ex.Message.ToString()));
            }

            return Ok(result);
        }
    }
}