-- Clientes
IF NOT EXISTS (SELECT *
FROM Cliente)
BEGIN    INSERT INTO Cliente
        (Nombre, Apellidos, Direccion, Telefono)
    VALUES        ('Juan', 'Perez', 'Calle asd 123', '+56 9 7834 2167')
    INSERT INTO Cliente
        (Nombre, Apellidos, Direccion, Telefono)
    VALUES        ('Lorena', 'Gomez', 'Calle dsa 213', '+56 9 7843 2617')
    INSERT INTO Cliente
        (Nombre, Apellidos, Direccion, Telefono)
    VALUES        ('Fernando', 'Lopez', 'Calle ads 321', '+56 9 7384 2176')
END
-- Cuentas
IF NOT EXISTS (SELECT *
FROM Cuenta)
BEGIN    INSERT INTO Cuenta
        (Nombre, Numero, ClienteId)
    VALUES        ('Cuenta de Juan', '000123123', 1)
    INSERT INTO Cuenta
        (Nombre, Numero, ClienteId)
    VALUES        ('Cuenta de Lorena', '000543513', 2)
    INSERT INTO Cuenta
        (Nombre, Numero, ClienteId)
    VALUES        ('Cuenta de Fernando', '000948292', 3)
END
-- Productos
IF NOT EXISTS (SELECT *
FROM Producto)
BEGIN    INSERT INTO Producto
        (Nombre)
    VALUES        ('Mouse')
    INSERT INTO Producto
        (Nombre)
    VALUES        ('Monitor')
    INSERT INTO Producto
        (Nombre)
    VALUES        ('Computador')
END
-- Precios
IF NOT EXISTS (SELECT *
FROM Precio)
BEGIN    INSERT INTO Precio
        (ProductoId, Valor)
    VALUES        (1, 30.99)
    INSERT INTO Precio
        (ProductoId, Valor)
    VALUES        (2, 25.50)
    INSERT INTO Precio
        (ProductoId, Valor)
    VALUES        (3, 790.75)
END
