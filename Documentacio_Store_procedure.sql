
-- 1. ESTRUCTURA DE TABLAS

CREATE TABLE Producto (
    idProducto INT PRIMARY KEY,
    nombre VARCHAR(50),
    precio MONEY,
    existencia INT
);

CREATE TABLE Venta (
    idventa INT PRIMARY KEY IDENTITY(1,1),
    fecha DATETIME,
    idcliente INT,
    total MONEY
);

CREATE TABLE DetalleVenta (
    idventa INT,
    idProducto INT,
    precioVenta MONEY,
    cantidad INT
);


-- 2. CREACIÓN DEL TIPO DE TABLA (TYPE)

CREATE TYPE dbo.DetalleVentaType AS TABLE (
    idProducto INT,
    cantidad INT,
    precioVenta MONEY
);
GO

-- 3. STORED PROCEDURE (INSERCIÓN MÚLTIPLE)

CREATE PROCEDURE sp_InsertarVentaCompleta
    @idcliente INT,
    @total MONEY,
    @Detalles dbo.DetalleVentaType READONLY 
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION
    BEGIN TRY
        
        INSERT INTO Venta (fecha, idcliente, total) 
        VALUES (GETDATE(), @idcliente, @total);
        
        DECLARE @idv INT = SCOPE_IDENTITY();

        
        INSERT INTO DetalleVenta (idventa, idProducto, precioVenta, cantidad)
        SELECT @idv, idProducto, precioVenta, cantidad FROM @Detalles;

        
        UPDATE P SET P.existencia = P.existencia - D.cantidad
        FROM Producto P 
        INNER JOIN @Detalles D ON P.idProducto = D.idProducto;

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        DECLARE @Msg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@Msg, 16, 1);
    END CATCH
END
GO