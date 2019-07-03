using System;
using System.Collections.Generic;
using System.Linq;
using NexusReportingApi.Models;

namespace NexusReportingApi.API {
    public class DataSeed{
        private readonly ApiContext _ctx;
        public DataSeed(ApiContext ctx){
            _ctx = ctx;
        }

        public void SeedData(int nCustomers, int nOrders){

            if(!_ctx.Customers.Any()){
                SeedCustomers(nCustomers);
                 _ctx.SaveChanges();
            }
            if(!_ctx.Orders.Any()){
                SeedOrders(nOrders);
                 _ctx.SaveChanges();
            }
            if(!_ctx.Servers.Any()){
                SeedServers();
                  _ctx.SaveChanges();
            }


          
        }

        private void SeedCustomers(int nCustomers){
            List<Customer> customers = BuildCustomerList(nCustomers);

            foreach(var customer in customers){
                _ctx.Customers.Add(customer);
            }
        }
         private List<Customer> BuildCustomerList(int nCustomers){
             var customers = new List<Customer>();
             for(var i = 1; i<=nCustomers; i++){

                 var name = Helpers.MakeCustomerName();
                 customers.Add(new Customer{
                     Id = i,
                     Name = name,
                     Email = Helpers.MakeCustomerEmail(name),
                     State = Helpers.MakeCustomerState()
                 });
             }
             return customers;
         }

    private List<Order> BuildOrderList(int nOrders){
             var orders = new List<Order>();
             var rand = new Random();
             for(var i = 1; i<=nOrders; i++){

                 var placed = Helpers.GetRandomOrderPlaced();
                 var completed = Helpers.GetRandomOrderComplete(placed);
                 var randCustomerId = rand.Next(_ctx.Customers.Count());

                 orders.Add(new Order{
                    Id = i,
                    Customer = _ctx.Customers.Where(c => c.Id == randCustomerId).FirstOrDefault(),
                    Completed = completed,
                    Placed = placed,
                    orderTotal = Helpers.GetRandomOrderTotal()
                 });
             }
             return orders;
         }

    private  List<Server> BuildServerList(){

            var servers = new List<Server>(){
                new Server{Id = 1, Name = "Dev-1", isHealthy=true},
                new Server{Id = 2, Name = "QA-1", isHealthy=true},
                new Server{Id = 3, Name = "Stage-1", isHealthy=false},
                new Server{Id = 4, Name = "Prod-1", isHealthy=true},
                new Server{Id = 5, Name = "Dev-2", isHealthy=true},
                new Server{Id = 6, Name = "QA-2", isHealthy=true},
                new Server{Id = 7, Name = "Stage-2", isHealthy=false},
                new Server{Id = 8, Name = "Prod-2", isHealthy=true},



            };

            return servers;
        }

        private void SeedOrders(int nOrders){
            List<Order> orders = BuildOrderList(nOrders);

            foreach(var o in orders){
                _ctx.Orders.Add(o);
            }

        }
        private void SeedServers(){

            List<Server> servers = BuildServerList();
            foreach(var s in servers){
                _ctx.Servers.Add(s);
            }
        }
    }
}