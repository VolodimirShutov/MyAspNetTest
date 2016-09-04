﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.IO;

using BeforeTest.Models;

namespace TestApplication.Models
{
    public class FilesController : ApiController
    {
        private static SizeModel _size;

        public SizeModel GetAllFiles()
        {

            string path = "D:\\";

            _size = new SizeModel();
            _size.initClass();

            DirectoryInfo derictories = new DirectoryInfo(path);
            _size.fullPath = derictories.FullName;
            if (derictories.Parent != null)
            {
                _size.parentPath = derictories.Parent.FullName;
            }

            startChekFile(path);

            createFilesInfo(path);
            createDirectoriesInfo(path);

            return _size;
        }

        public IHttpActionResult GetFiles(int id)
        {
            string path = "D:\\work\\";
            if (id == -1 || _size == null)
                path = "D:\\work\\";
            else
            {
                foreach (var directory in _size.directories)
                {
                    if (directory.id == id)
                    {
                        path = directory.path + "\\";
                        break;
                    }
                }
            }

            _size = new SizeModel();
            _size.initClass();

            DirectoryInfo derictories = new DirectoryInfo(path);
            _size.fullPath = derictories.FullName;

            if (derictories.Parent != null)
            {
                _size.parentPath = derictories.Parent.FullName;
            }

            startChekFile(path);

            createFilesInfo(path);
            createDirectoriesInfo(path);

            return Ok(_size);
            //return Ok("GetProduct good" + id);
        }

        private void createFilesInfo(string path)
        {
            DirectoryInfo files = new DirectoryInfo(path);
            List<FileModel> filesList = new List<FileModel>();
            foreach (var fileItem in files.GetFiles())
            {
                FileModel file = new FileModel();
                file.name = fileItem.Name;
                filesList.Add(file);
            }
            _size.files = filesList;
        }

        private void createDirectoriesInfo(string path)
        {

            DirectoryInfo directories = new DirectoryInfo(path);

            List<DirectoriesModel> directoriesList = new List<DirectoriesModel>();
            int counter = 0;

            DirectoriesModel directory = new DirectoriesModel();
            if (directories.Parent != null)
            {
                directory.name = "...";
                directory.path = _size.parentPath;
                directory.id = counter;
                directoriesList.Add(directory);
            }

            foreach (var derictoryItem in directories.GetDirectories())
            {
                counter++;
                directory = new DirectoriesModel();
                directory.name = derictoryItem.Name;
                directory.path = derictoryItem.FullName;
                directory.id = counter;
                directoriesList.Add(directory);
            }
            _size.directories = directoriesList;
        }

        private void startChekFile(string path)
        {
            checkDirectori(path);
            checkFiles(path);
        }

        private void checkDirectori(string path)
        {
            DirectoryInfo derictories = new DirectoryInfo(path);
            string nextPath;
            String derictoryName;
            foreach (var derictori in derictories.GetDirectories())
            {
                derictoryName = derictori.Name;
                nextPath = path + derictoryName + "\\";
                startChekFile(nextPath);
            }
        }

        private void checkFiles(string path)
        {
            DirectoryInfo files = new DirectoryInfo(path);
            long fileSize;
            foreach (var file in files.GetFiles())
            {
                fileSize = file.Length;
                if (fileSize < 10000000)
                {
                    _size.lessTen++;
                }
                else if (fileSize > 100000000)
                {
                    _size.ten++;
                }
                else
                {
                    _size.moreTen++;
                }
            }
        }
    }
}
