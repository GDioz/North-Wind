namespace northWind.Infrastructure.Scripts
{
    public static class NorthWindScripts
    {
        public const string SQL_OBTER_PRODUTO_POR_ANO = @"
            SELECT
                YEAR(ShippedDate) AS Year,
                O.OrderID AS OrderId,
                SUM(UnitPrice * Quantity) AS Subtotal,
                ShippedDate
            FROM 
                dbo.Orders O 
            INNER JOIN 
                dbo.[Order Details] D ON O.OrderID = D.OrderID
            WHERE 
                YEAR(@year) = YEAR(ShippedDate)
                AND YEAR(@year) BETWEEN YEAR('1996-12-24') AND YEAR('1997-09-30')
            GROUP BY
                YEAR(ShippedDate),
                O.OrderID,
                ShippedDate";

        public const string SQL_OBTER_ORDERS = @"
            SELECT TOP 500
                OrderID as OrderID,
                CustomerID as CustomerID,
                OrderDate as OrderDate,
                ShippedDate as ShippedDate,
                ShipName as ShipName
            FROM 
                dbo.Orders
        ";

        public const string SQL_OBTER_PRODUTOS = @"
            SELECT TOP 500
                ProductID as ProductID,
                ProductName as ProductName,
                UnitPrice as UnitPrice
            FROM 
                dbo.Products
            WHERE 
                UnitPrice <  @price"  
        ;

        public const string SQL_OBTER_PRODUTO_ACIMA_DA_MEDIA_PRECO = @"
            SELECT TOP 500
                ProductID as ProductID,
                ProductName as ProductName,
                UnitPrice as UnitPrice
            FROM 
                dbo.Products
            WHERE
                UnitPrice > (SELECT AVG(UnitPrice) From dbo.Products)
            ORDER BY 
                UnitPrice ASC
        ";

        public const string SQL_OBTER_VALOR_TOTAL_PEDIDO = @"
            SELECT 
                OrderID as OrderID,
                SUM(UnitPrice * Quantity) as Subtotal
            FROM 
                dbo.[Order Details]
            GROUP BY 
                OrderID
        ";

        public const string SQL_OBTER_VENDA_POR_PAIS = @"
            SELECT 
                E.Country, E.LastName, E.FirstName, O.ShippedDate, O.OrderID,
                ROUND(SUM(D.Quantity*D.UnitPrice),2) AS SaleAmount
            FROM 
                dbo.Employees E
            INNER JOIN 
                dbo.Orders O ON E.EmployeeID = O.EmployeeID
            INNER JOIN 
                dbo.[Order Details] D ON O.OrderID = D.OrderID
            GROUP BY 
                E.Country, E.LastName, E.FirstName, O.OrderID, O.ShippedDate
        ";
    }
}
