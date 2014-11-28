using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Linq;

namespace ENC2.Models
{
    public class NodeRepository:INodeRepository
    {
        private DataContext db = new DataContext("Data Source = achilles; Initial Catalog=ENC; Integrated Security=False; User ID=ENC;Password=ENC;");
        
        public Nodes Get(string hostname)
        {
            Table<Nodes> tblnodes = db.GetTable<Nodes>();
            Nodes node;
            node = (from n in tblnodes
                        where n.Node == hostname
                        select n).First();
            return node;
        }

        public void Add(Nodes item)
        {
            Table<Nodes> tblNodes = db.GetTable<Nodes>();
            tblNodes.InsertOnSubmit(item);
            db.SubmitChanges();
            
        }

        public void Remove(string hostname)
        {
            Table<Nodes> tblNodes = db.GetTable<Nodes>();
            var deletenode =
                (from n in tblNodes
                 where n.Node == hostname
                 select n).First();
            tblNodes.DeleteOnSubmit(deletenode);
            db.SubmitChanges();
        }

        public void Update(Nodes item)
        {
            Table<Nodes> tblNodes = db.GetTable<Nodes>();
            var updatenode =
                (from n in tblNodes
                 where n.Node == item.Node
                 select n).First();
            updatenode.Enctext = item.Enctext;
            db.SubmitChanges();
            return;
        }



    }




}