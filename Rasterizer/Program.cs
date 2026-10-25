using System;
using System.Diagnostics;

// TODO: Añadir validación para el path de los archivos creados

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Action> argumentMethodMapper = new()
            {
                { "TestImage", RenderTestImage },
                { "SingleTrinagle", RenderSingleTriangle },
                { "RandomTrinagles", RenderRandomTriangles },
                { "Cube", RenderPerspectiveCube }
            };

            if (argumentMethodMapper.ContainsKey(args[0]))
            {
                Action mappedMethod;
                argumentMethodMapper.TryGetValue(args[0], out mappedMethod);
                mappedMethod?.Invoke();
            }
            else
            {
                Console.WriteLine("Wrong argument, input a valid demo name");
            }
        }

        static void RenderTestImage()
        {
            const int width = 1024;
            const int height = 1024;
            const string folderPath = ".\\Output\\TestImg\\";
            float3[,] image = TestImage.DrawImage(width, height);
            BMPFileWriter.BuildSingleFile(image, folderPath);
            FileExtensionChanger.ChangeFilesExtensionInFolder(folderPath, ".bmp");
        }

        static void RenderSingleTriangle()
        {
            const int width = 1024;
            const int height = 1024;
            const string folderPath = ".\\Output\\SingleTriangle\\";
            float3[,] image = FirstTriangle.DrawTriangle(width, height);
            BMPFileWriter.BuildSingleFile(image, folderPath);
            FileExtensionChanger.ChangeFilesExtensionInFolder(folderPath, ".bmp");
        }

        static void RenderRandomTriangles()
        {
            const int width = 1024;
            const int height = 1024;
            const int numberOfFrames = 250;
            const int numberOfTriangles = 50;
            const string folderPath = ".\\Output\\RandomTriangles\\";
            RandomTriangles drawer = new RandomTriangles(width, height, numberOfTriangles);
            Console.WriteLine("Starting render");
            for (int i = 0; i < numberOfFrames; i++)
            {
                float3[,] image = drawer.DrawTriangles();
                if (i > 0) drawer.AdvanceTriangles();
                BMPFileWriter.BuildSingleFile(image, folderPath, "frame" + (i + 1) + ".txt", i);
            }
            Console.WriteLine("Render finished");
            Console.WriteLine("Converting files");
            FileExtensionChanger.ChangeFilesExtensionInFolder(folderPath, ".bmp");
            Console.WriteLine("Conversion finished");

        }

        static void RenderPerspectiveCube()
        {
            const int width = 1024;
            const int height = 1024;
            const float cameraFiledOfView = 120f;
            const int numberOfFrames = 180;
            const string cubeObjFilePath = ".\\Resources\\Cube.obj";
            const string folderPath = ".\\Output\\PerspectiveCube\\";

            FolderCleaner.CleanFolder(folderPath);


            RenderObject cube = ObjParser.ToRenderObject(cubeObjFilePath);
            Camera camera = new(new(0, 0, 0), cameraFiledOfView, new(width, height));
            PerspectiveCubeDrawer drawer = new(width, height, cube);
            float3 cubePosition = new(0f, 0f, 2f);
            float3 cubeRotation = new(0f, 0f, 0f);
            cube.UpdateObjectPosition(cubePosition);

            Console.WriteLine("Starting render");
            for (int i = 0; i < numberOfFrames; i++)
            {
                long startTime = Stopwatch.GetTimestamp();

                cubeRotation.y += 2;
                float3[,] image = drawer.DrawCube(camera);
                cube.RotateObject(cubeRotation);

                BMPFileWriter.BuildSingleFile(image, folderPath, "frame" + (i + 1) + ".txt", i);

                TimeSpan elapsedTime = Stopwatch.GetElapsedTime(startTime);
                Console.WriteLine("Frame " + i + " rendered in " + elapsedTime.Seconds + " s");
            }
            Console.WriteLine("Render finished");
            Console.WriteLine("Converting files");
            FileExtensionChanger.ChangeFilesExtensionInFolder(folderPath, ".bmp");
            Console.WriteLine("Conversion finished");
        }
    }
}
