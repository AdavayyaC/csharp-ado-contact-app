-- ============================================
-- SQL Setup Script for ADO.NET Contact App
-- Database: MySQL 8.0
-- ============================================

-- Step 1: Create the database
CREATE DATABASE IF NOT EXISTS ContactDB;

-- Step 2: Switch to the new database
USE ContactDB;

-- Step 3: Create the Contacts table
CREATE TABLE IF NOT EXISTS Contacts (
    ContactId    INT AUTO_INCREMENT PRIMARY KEY,
    FirstName    VARCHAR(40)  NOT NULL,
    LastName     VARCHAR(40)  NOT NULL,
    Email        VARCHAR(50)  NOT NULL,
    PhoneNumber  VARCHAR(20)  NOT NULL,
    Address      VARCHAR(80)  NOT NULL,
    WebAddress   VARCHAR(50)  NOT NULL,
    Notes        VARCHAR(80)  NOT NULL
);

-- Step 4: Insert sample data (optional)
INSERT INTO Contacts (FirstName, LastName, Email, PhoneNumber, Address, WebAddress, Notes)
VALUES 
    ('Rahul', 'Kumar', 'rahul@example.com', '9876543210', 'Bangalore, Karnataka', 'www.rahul.dev', 'Classmate'),
    ('Priya', 'Sharma', 'priya@example.com', '8765432109', 'Mumbai, Maharashtra', 'www.priya.in', 'Project Partner'),
    ('Amit', 'Patil', 'amit@example.com', '7654321098', 'Pune, Maharashtra', 'www.amit.tech', 'Senior');

-- Verify the data
SELECT * FROM Contacts;
