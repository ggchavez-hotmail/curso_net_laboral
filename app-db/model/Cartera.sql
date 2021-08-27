CREATE TABLE [dbo].[Cartera]
(
 [CarteraId] INT NOT NULL PRIMARY KEY IDENTITY,
 [ProductoId] INT FOREIGN KEY REFERENCES Producto(ProductoId),
 [CuentaId] INT FOREIGN KEY REFERENCES Cuenta(CuentaId),
 [MontoTotal] DECIMAL(20,6) NOT NULL CHECK(MontoTotal > 0),
 [Cantidad] INT NOT NULL CHECK(Cantidad > 0),
)
