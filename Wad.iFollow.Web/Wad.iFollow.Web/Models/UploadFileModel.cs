using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Wad.iFollow.Web.Models
{
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;

        public FileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            if (value == null) 
                return true;
            
            return _maxSize > (value as HttpPostedFileWrapper).ContentLength;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("The file size should not exceed {0}", _maxSize);
        }
    }

    public class FileTypesAttribute : ValidationAttribute
    {
        private readonly List<string> _types;

        public FileTypesAttribute(string types)
        {
            _types = types.Split(',').ToList();
        }

        public override bool IsValid(object value)
        {
            if (value == null) 
                return true;

            var fileExt = System.IO.Path.GetExtension((value as HttpPostedFileWrapper).FileName).Substring(1);
            return _types.Contains(fileExt, StringComparer.OrdinalIgnoreCase);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Invalid file type. Only the following types {0} are supported.", String.Join(", ", _types));
        }
    }

    public class MessageOtherThan : ValidationAttribute
    {
        string _msg;

        public MessageOtherThan(string msg)
        {
            _msg = msg;
        }

        public override bool IsValid(object value)
        {
            if (value == null) 
                return false;

            var objectMsg = value as string;
            return objectMsg != _msg;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Add a comment.");
        }
    }

    public class UploadFileModel
    {
        [FileSize(1024*1024*3)]
        [FileTypes("jpg,jpeg,png,gif")]
        public HttpPostedFileBase File { get; set; }

        [MessageOtherThan("Write a comment...")]
        public string Message { get; set; }

        public string Path { get; set; }
    }
}