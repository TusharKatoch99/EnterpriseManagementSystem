-- ===========================================
-- Enterprise Management System (EMS)
-- Database Creation Script
-- Author : Tushar
-- ===========================================

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'EMS_DB')
BEGIN
    CREATE DATABASE EMS_DB;
END
GO

USE EMS_DB;
GO