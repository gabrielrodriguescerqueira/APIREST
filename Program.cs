using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapPost("/products", (Product product) => {
    ProductRespository.Add(product);
    return Results.Created("/products/" + product.Code, product.Code);
});

app.MapGet("/products/{code}", ([FromRoute] string code) => {
    var product = ProductRespository.GetBy(code);
    if(product != null)
    return Results.Ok(product);
 return Results.NotFound();

});

app.MapPut("/products",(Product product) => {
    var productSaved = ProductRespository.GetBy(product.Code);
    productSaved.Name = product.Name;
    return Results.Ok();
});

app.MapDelete("/products/{code}", ([FromRoute] string code ) =>{
    var productSaved = ProductRespository.GetBy(code);
    ProductRespository.Remove(productSaved);
    return Results.Ok();
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
