﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Console;
using Domain;
using FluentNHibernate.Testing;
using NHibernate;
using NUnit.Framework;
using System.Collections;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        private ISessionFactory _sessionFactory;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            HibernatingRhinos.NHibernate.Profiler.Appender.NHibernateProfiler.Initialize();
            _sessionFactory = Setup.CreateSessionFactory();
        }

        [Test]
        public void Test1()
        {
            using (var session = _sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var customer = new Customer()
                {
                    FirstName = "Paul",
                };

                var geb = new Book {Name = "Godel, Escher, Bach"};
                var clr = new Book {Name = "CLR via C#"};

                customer.FavouriteBooks[BookType.PopSci] = geb;
                customer.FavouriteBooks[BookType.Programming] = clr;
                
                session.Save(customer);
                tx.Commit();
            }


        }

    }
}
