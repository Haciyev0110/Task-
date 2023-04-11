# Task- 

Task .net core 5 environment də yazılıb, Web Api Restfull isdifadə olunub.

Əlavə olunan packges : Nlog.Extensions.Logging, NlogWeb.AspNetCore, Newtonsoft.Json, System.Data.SqlClient.

Databse ilə  və modellə ilə əməliyyatlər aydı dll-lərə çıxarılıb : DB və Container .

Respone  BaseResponse class  ilə qaytarılır , standart web api status ilə qaytarıla bilər lakin business məntiqdən aslı olaraq BaseResponse class isdifadə eləmək olar.

Database MSSql database isdifadə olunub.


Search sql function :


USE [TestDB]
GO
/****** Object:  UserDefinedFunction [dbo].[SearchTrademarkByWord]    Script Date: 4/11/2023 12:06:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER function [dbo].[SearchTrademarkByWord]
(
    @SearchWord NVARCHAR(100)
)
RETURNS table
AS
RETURN 
    SELECT TrademarkID, TrademarkLogoUrl, TrademarkName, TrademarkClasses, TrademarkStatus1, TrademarkStatus2, TrademarkDetailsPage
    FROM Trademarks
    WHERE TrademarkName LIKE '%' + @SearchWord + '%';



