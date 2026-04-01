-- CREACIÓN DE TABLAS 

CREATE TABLE Cliente (
    idcliente INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(100) NOT NULL,
    pais VARCHAR(50),
    ciudad VARCHAR(50)
);

CREATE TABLE Producto (
    idProducto INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(100) NOT NULL,
    precio MONEY NOT NULL,
    existencia INT NOT NULL
);

CREATE TABLE Venta (
    idventa INT PRIMARY KEY IDENTITY(1,1),
    fecha DATETIME DEFAULT GETDATE(),
    idcliente INT,
    CONSTRAINT FK_Venta_Cliente FOREIGN KEY (idcliente) REFERENCES Cliente(idcliente)
);

CREATE TABLE DetalleVenta (
    idventa INT NOT NULL,
    idProducto INT NOT NULL,
    precioVenta MONEY NOT NULL,
    cantidad INT NOT NULL,
    CONSTRAINT PK_DetalleVenta PRIMARY KEY (idventa, idProducto),
    CONSTRAINT FK_DV_Venta FOREIGN KEY (idventa) REFERENCES Venta(idventa),
    CONSTRAINT FK_DV_Producto FOREIGN KEY (idProducto) REFERENCES Producto(idProducto)
);
GO