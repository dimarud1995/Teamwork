/*
Скрипт развертывания для E:\WORKSPACE\A SITE VERSIONS\STABLE V4.0\TEAMWORK\SVTRADE\APP_DATA\TRADEDB.MDF

Этот код был создан программным средством.
Изменения, внесенные в этот файл, могут привести к неверному выполнению кода и будут потеряны
в случае его повторного формирования.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "E:\WORKSPACE\A SITE VERSIONS\STABLE V4.0\TEAMWORK\SVTRADE\APP_DATA\TRADEDB.MDF"
:setvar DefaultFilePrefix "E_\WORKSPACE\A SITE VERSIONS\STABLE V4.0\TEAMWORK\SVTRADE\APP_DATA\TRADEDB.MDF_"
:setvar DefaultDataPath "C:\Users\gostw\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\v11.0\"
:setvar DefaultLogPath "C:\Users\gostw\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\v11.0\"

GO
:on error exit
GO
/*
Проверьте режим SQLCMD и отключите выполнение скрипта, если режим SQLCMD не поддерживается.
Чтобы повторно включить скрипт после включения режима SQLCMD выполните следующую инструкцию:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Для успешного выполнения этого скрипта должен быть включен режим SQLCMD.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Обновление завершено.';


GO
delete from Article
delete from ChoosedCategory
delete from Order
delete from OrderStatus
delete from Product
delete from ProductToBuy
delete from ReservedProduct
delete from ShowedProduct
delete from User
delete from UserGroup