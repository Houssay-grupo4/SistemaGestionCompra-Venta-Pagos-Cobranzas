create database Negocio 

 CREATE TABLE Ventas (
   
    VentaID INT PRIMARY KEY,
    Codigo_venta VARCHAR(50), 
    Fecha_venta DATE,
    Total_venta DECIMAL(10, 2), 
    ListaProductos VARCHAR(MAX), 
    ClienteID INT 
);

  create table Compras(
    PedidoID INT PRIMARY KEY,
    FechaPedido DATE,
    ProveedorID INT,
    DetallesPedido VARCHAR(MAX),
 );

create table Cobranzas(
    CobroID INT PRIMARY KEY,
    FechaCobro DATE,
    MontoCobrado DECIMAL(10, 2), 
    TipoDocumento VARCHAR(50), 
    DetallesDocumento VARCHAR(MAX) 
);


create table Pagos(
    PagoID INT PRIMARY KEY,
    FechaPago DATE,
    MontoPago DECIMAL(10, 2), 
    DetallesPago VARCHAR(MAX) 
);

CREATE TABLE Productos (
    ProductoID INT PRIMARY KEY,
    Codigo_producto VARCHAR(50),
    Nombre VARCHAR(255), 
    Cantidad INT,
    Precio DECIMAL(10, 2) 
);

CREATE TABLE Clientes (
   
    ClienteID INT PRIMARY KEY,
    Dni_cliente INT, 
    NombreCompleto VARCHAR(255), 
    Telefono VARCHAR(20), 
    Email VARCHAR(255),
);
