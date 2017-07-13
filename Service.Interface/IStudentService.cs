using System;
using System.Collections.Generic;
using Data.Entities;

namespace Service.Interface
{
    public interface IStudentService
    {
        void Add(Student student);
        void Delete(Guid id);
        Data.Entities.Student Get(Guid id);
         IList<Student> FindAll(Func<Student,bool>func);
    }
}