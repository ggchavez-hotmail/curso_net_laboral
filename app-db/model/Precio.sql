CREATE TABLE [dbo].[Precio]
(
 [PrecioId] INT NOT NULL PRIMARY KEY IDENTITY,
 [ProductoId] INT FOREIGN KEY REFERENCES Producto(ProductoId),
 [Valor] DECIMAL(20,6) NOT NULL CHECK(Valor > 0),
)
