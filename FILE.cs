﻿using System;
using System.IO;

class FILE
{
    public static FileStream fopen(string filename, string mode)
    {
        FileStream file = null;
        
        switch (mode)
        {
            case "r":
                file = File.OpenRead(filename);
                break;
            case "w":
                file = File.OpenWrite(filename);
                break;
        }

        return file;
    }

    public static int fseek(FileStream stream, long offset, int origin)
    {
        var newpositioninthestream = stream.Seek(offset, (SeekOrigin)origin);

        return (int)newpositioninthestream;
    }

    public static int ftell(FileStream pfile)
    {
        return pfile.te
    }
}