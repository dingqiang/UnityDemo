using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Microsoft.Practices.Unity;
using Repository.Interface;
using Service.Interface;

namespace Service
{
    public class StudentService : IStudentService
    {
        [Dependency]
        public IStudentRepository StudentRepository { get; set; }
        
        public Student Get(Guid id)
        {
            return StudentRepository.Get(id);
        }

        public void Delete(Guid id)
        {
            StudentRepository.Delete(id);
        }

        public void Add(Student student)
        {
            StudentRepository.Add(student);
        }

        public IList<Student> FindAll(Func<Student, bool> func)
        {
            if (func == null) return StudentRepository.FindAll(c => true).ToList();
            return StudentRepository.FindAll(func).ToList();
        }
    }
}