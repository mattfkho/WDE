using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WDE.Models
{
    //public partial class TrolleyExerciseRequest
    //{
    //    [JsonProperty("token", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    //    public System.Guid? Token { get; set; }
    //    [JsonProperty("url", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    //    public string Url { get; set; }
    //}

    //public partial class ExerciseResult
    //{
    //    [JsonProperty("passed", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    //    public bool? Passed { get; set; }
    //    [JsonProperty("url", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    //    public string Url { get; set; }
    //    [JsonProperty("message", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    //    public string Message { get; set; }
    //}

    /// <summary>
    /// Shopper Orders
    /// </summary>
    public partial class ShopperOrders
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        [JsonProperty("customerId", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public int? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        [JsonProperty("products", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public List<Product> Products { get; set; }
    }

    /// <summary>
    /// The Product
    /// </summary>
    public partial class Product
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [JsonProperty("price", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Price { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [JsonProperty("quantity", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Quantity { get; set; }
    }

    /// <summary>
    /// The Trolley
    /// </summary>
    public partial class Trolley
    {
        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        [JsonProperty("products", Required = Required.Always)]
        [Required]
        public List<TrolleyProduct> Products { get; set; } = new List<TrolleyProduct>();

        /// <summary>
        /// Gets or sets the specials.
        /// </summary>
        /// <value>
        /// The specials.
        /// </value>
        [JsonProperty("specials", Required = Required.Always)]
        [Required]
        public List<Special> Specials { get; set; } = new List<Special>();

        /// <summary>
        /// Gets or sets the quantities.
        /// </summary>
        /// <value>
        /// The quantities.
        /// </value>
        [JsonProperty("quantities", Required = Required.Always)]
        [Required]
        public List<ProductQuantity> Quantities { get; set; } = new List<ProductQuantity>();
    }

    /// <summary>
    /// Trolley Product
    /// </summary>
    public partial class TrolleyProduct
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [JsonProperty("price", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Price { get; set; }
    }

    /// <summary>
    /// Special
    /// </summary>
    public partial class Special
    {
        /// <summary>
        /// Gets or sets the quantities.
        /// </summary>
        /// <value>
        /// The quantities.
        /// </value>
        [JsonProperty("quantities", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public List<ProductQuantity> Quantities { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [JsonProperty("total", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Total { get; set; }
    }

    /// <summary>
    /// Product Quantity
    /// </summary>
    public partial class ProductQuantity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [JsonProperty("quantity", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public int? Quantity { get; set; }
    }

    /// <summary>
    /// The User
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("token", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
    }
}