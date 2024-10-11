﻿using System;
using System.IO;
using System.Reflection;

namespace Logger;

public class LogFactory
{
    private string? FilePath { get; set; }

    public void ConfigureFileLogger(string? filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            ArgumentNullException.ThrowIfNull(filePath);
        }
        FilePath = filePath;
    }

    public BaseLogger? CreateLogger(string className)
    {
        if (string.IsNullOrEmpty(FilePath))
        {
            ArgumentNullException.ThrowIfNull(FilePath);  // Ensure that null is handled
        }

        return new FileLogger(FilePath)
        {
            ClassName = className
        };
    }
    
    
    public static string? GetSolutionDirectory()
    {
        // Start from the current working directory
        string? currentDirectory = Directory.GetCurrentDirectory();

        while (currentDirectory != null)
        {
            // Check if a .sln file exists in the current directory
            if (Directory.GetFiles(currentDirectory, "*.sln").Length > 0)
            {
                return currentDirectory;
            }


            // Move up to the parent directory
            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
        }

        return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }

}