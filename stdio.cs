using System.IO;

static class stdio
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

    public static int fseek(ref FileStream stream, long offset, int origin)
    {
        stream.Seek(offset, (SeekOrigin)origin);

        return 0;
    }

    public static long ftell(FileStream stream)
    {
        return stream.Position;
    }

    public static void rewind(ref FileStream stream)
    {
        fseek(ref stream, 0, (int)SeekOrigin.Begin);
    }
}