using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/", () => new {Name ="Gabriel Rodrigues", Age = 21});
app.MapGet("/AddHeader", (HttpResponse response) => response.Headers.Add("Teste", "Gabriel Rodrigues"));

app.MapPost("/saveproduct", (Product product) => {
    return product.Code + " - " + product.Name;
});

//api.app.com/users?datastart={date}&dataend={date}
app.MapGet("/getproduct", ([FromQuery] string dateStart, [FromQuery] string dateEnd) => {
    return dateStart + " - " + dateEnd;
});

//api.app.com/user/{code}
app.MapGet("/getproduct/{code}", ([FromRoute] string code) => {
    return code;
});

app.MapGet("/getproductbyheader",(HttpRequest request) => {
    return request.Headers["product-code"].ToString();
});

app.MapPut("/editproduct", (Product product) => {
    var productSaved = ProductRespository.GetBy(product.Code);
    productSaved.Name = product.Name;
});

app.MapDelete("/deleteproduct/{code}", ([FromRoute] string code ) =>{
    var productSaved = ProductRespository.GetBy(code);
    ProductRespository.Remove(productSaved);
});

app.Run();

public static class ProductRespository {
    public static List<Product> Products {get ; set;}

    public static void Add(Product product) {
        if(Products == null)
         Products = new List<Product>();

         Products.Add(product);
    }

    public static Product GetBy(string code) {
       return Products.FirstOrDefault(p => p.Code == code);
    }
    public static void Remove (Product product) {
        Products.Remove(product);
    }
}


 
public class Product {
    public string Code {get; set; }
    public string Name {get; set; }
}
