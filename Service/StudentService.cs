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

        public IList<Student> FindAll(string name)
        {
            return StudentRepository.FindAll(c => c.Name.Contains(name)).ToList();
        }

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
    }
}