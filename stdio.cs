﻿using System.IO;

static class stdio
{
    static void printf(string format)
    {
        Console.Write(format);
    }

    static int fseek(Stream stream, long offset, int origin)
    {
        try
        {
            stream.Seek(offset, (SeekOrigin)origin);
            return 0;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    static uint fread(byte[] ptr, uint size, uint count, Stream stream)
    {
        if (ptr == null || stream == null || size <= 0 || count <= 0)
        {
            return 0;
        }

        try
        {
            var bufferSize = size * count;
            var bytesRead = (uint)stream.Read(ptr, 0, (int)bufferSize);
            var elementsRead = bytesRead / size;
            return elementsRead;
        }
        catch (Exception)
        {
            // Handle any exceptions that occur during reading
            return unchecked((uint)-1);
        }
    }

    static uint fwrite(byte[] ptr, uint size, uint count, Stream stream)
    {
        if (ptr == null || size < 1 || count < 1 || stream == null)
        {
            return 0;
        }

        using var writer = new BinaryWriter(stream);
        var elementsToWrite = size * count;
        var bytesToWrite = Math.Min(ptr.Length, elementsToWrite);

        writer.Write(ptr, 0, (int)bytesToWrite);
        writer.Flush();

        return (uint)(bytesToWrite / size);
    }
    static FileStream fopen(string filename, string mode)
    {
        FileMode fileMode;
        FileAccess fileAccess;

        switch (mode)
        {
            case "r":
                fileMode = FileMode.Open;
                fileAccess = FileAccess.Read;
                break;
            case "w":
                fileMode = FileMode.Create;
                fileAccess = FileAccess.Write;
                break;
            case "a":
                fileMode = FileMode.Append;
                fileAccess = FileAccess.Write;
                break;
            case "r+":
                fileMode = FileMode.Open;
                fileAccess = FileAccess.ReadWrite;
                break;
            case "w+":
                fileMode = FileMode.Create;
                fileAccess = FileAccess.ReadWrite;
                break;
            case "a+":
                fileMode = FileMode.Append;
                fileAccess = FileAccess.ReadWrite;
                break;
            default:
                throw new ArgumentException("Invalid mode provided");
        }

        try
        {
            return new FileStream(filename, fileMode, fileAccess);
        }
        catch (Exception)
        {
            return null;
        }
    }

    static int fclose(FileStream stream)
    {
        if (stream == null)
        {
            return -1; // EOF is -1 in C#
        }

        try
        {
            stream.Flush();
            stream.Dispose();
            return 0;
        }
        catch
        {
            return -1;
        }
    }

    static int fseek(ref FileStream stream, long offset, int origin)
    {
        stream.Seek(offset, (SeekOrigin)origin);

        return 0;
    }

    static long ftell(FileStream stream)
    {
        return stream.Position;
    }

    static void rewind(ref FileStream stream)
    {
        fseek(ref stream, 0, (int)SeekOrigin.Begin);
    }
}