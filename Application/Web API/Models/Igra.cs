using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json.Serialization;


namespace DontGetSpicy.Models
{
    
    
    public struct MyRange{
        public int Start;
        public int End;
        public MyRange(int start, int end)
        {
            this.Start=(start>end)?end:start;
            this.End=(start>end)?start:end;
        }
        public bool isWithin(int questionableValue)
        {
            return questionableValue>=Start&&questionableValue<=End;
        }
        public List<int> getValues()
        {
            List<int> lista=new List<int>();
            for(int i=Start;i<=End;i++)
            {
                lista.Add(i);
            }
            return lista;
        }
    }
    public enum statusIgre{
        pauzirana,
        uToku,
        cekanjeIgraca
    }
   

    [Table("Igra")]
    public class Igra
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Status")]
        public statusIgre status { get; set; }
        [JsonIgnore]
        public Korisnik kreatorIgre { get; set; }
        public int crveniIgracId { get; set; }

        public int plaviIgracId { get; set; }

        public int zeleniIgracId { get; set; }

        public int zutiIgracId { get; set; }
        public string crveniUsername { get; set; }

        public string plaviUsername { get; set; }

        public string zeleniUsername { get; set; }

        public string zutiUsername { get; set; }
        public Boja naPotezu { get; set; }
        public string  accessCode { get; set; }
        public bool aleaIactaEst { get; set; }
        public List<Potez> potezi { get; set; }
        public List<Figura> figure { get; set; }
        public string groupNameGUID { get; set; }

       #region staticMembers
        

        [JsonIgnore]
        [NotMapped]
        public  static LinkedList<Boja> redosledPoteza=new LinkedList<Boja>(Enum.GetValues<Boja>());
        
        [JsonIgnore]
        [NotMapped]
       public static MyRange crveniOut=new MyRange(-4,-1);

       [JsonIgnore]
        [NotMapped]
       public static MyRange zeleniOut=new MyRange(-8,-5);

       [JsonIgnore]
        [NotMapped]
       public static MyRange zutiOut=new MyRange(-12,-9);

       [JsonIgnore]
        [NotMapped]
       public static MyRange plaviOut=new MyRange(-16,-13);
       
       [JsonIgnore]
        [NotMapped]
       public static MyRange crveniHome=new MyRange(53,56);

       [JsonIgnore]
        [NotMapped]
       public static MyRange zeleniHome=new MyRange(11,14);

       [JsonIgnore]
        [NotMapped]
       public static MyRange zutiHome=new MyRange(25,28);

       [JsonIgnore]
        [NotMapped]
       public static MyRange plaviHome=new MyRange(39,42);

       [JsonIgnore]
        [NotMapped]
       public static int mbr=14;
       #endregion staticMembers
       public static int generisiKocku()
        {   int minValue=1;
            int maxValue=7;
            if (minValue > maxValue) 
            throw new ArgumentOutOfRangeException("minValue");
            if (minValue == maxValue) return minValue;
            Int64 diff = maxValue - minValue;
            RNGCryptoServiceProvider _rng=new RNGCryptoServiceProvider();
            byte[] _uint32Buffer = new byte[4];
            while (true)
            {
            _rng.GetBytes(_uint32Buffer);
            UInt32 rand = BitConverter.ToUInt32(_uint32Buffer, 0);

            Int64 max = (1 + (Int64)UInt32.MaxValue);
            Int64 remainder = max % diff;
            if (rand < max - remainder)
            {
                return (Int32)(minValue + (rand % diff));
            }
            }
        }

        public bool imaLiSeStaOdigrati(Boja boja, int kocka)
       {
          List<Figura> isteBoje=figure.Where(Figura=>Figura.boja==boja).ToList<Figura>();
          foreach(Figura figura in isteBoje)
          {
              if(racunajPomeranje(figura,kocka)!=0)
                {
                    return true;
                }
          }
          return false;
            
       }
       public int racunajPomeranje(Figura figura, int kocka)
       {    
           
           if(figura.index<0&&kocka==6&&figure.Where(fig=>fig.index==((int)figura.boja*mbr+1)&&fig.boja==figura.boja).FirstOrDefault()==null)
           return (int)figura.boja*mbr+1;
           else
           if(figura.index>0)
           {
               int novaPozicija=(figura.index+kocka<57)?figura.index+kocka:(figura.index+kocka)%56;
            
            foreach(Boja b in Enum.GetValues(typeof (Boja)))
            {      
                
                   
                 if(b==figura.boja) continue;
                   
                MyRange bojaHome=(MyRange)typeof(Igra).GetField(b.ToString()+"Home").GetValue(null);

                if(bojaHome.isWithin(novaPozicija)||(figura.index==bojaHome.Start-2&&kocka==6)||(figura.index==bojaHome.Start-1&&kocka==6)||(figura.index==bojaHome.Start-1&&kocka==5))
                {   
                    novaPozicija+=4;
                    novaPozicija=(novaPozicija<57)?novaPozicija:(novaPozicija)%56;
                }
                 
            }
            if(figure.Where(fig=>fig.index==novaPozicija&&fig.boja==figura.boja).FirstOrDefault()!=null) return 0;
            if(novaPozicija>=(int)figura.boja*mbr+1&&figura.index<=((MyRange)typeof(Igra).GetField(figura.boja.ToString()+"Home").GetValue(null)).End&&figura.index>=((MyRange)typeof(Igra).GetField(figura.boja.ToString()+"Home").GetValue(null)).End-8)
                return 0;
            return (novaPozicija<57)?novaPozicija:(novaPozicija)%56;
           }
           return 0;
       }
       public List<Tuple<int,int>> odigrajPotez(Figura figura, int kocka, Boja bojaKorisnika)
       {
           List<Tuple<int,int>> potezi=new List<Tuple<int, int>>();
           if(figura.boja!=bojaKorisnika) return null;
            int novaPozicija=this.racunajPomeranje(figura,kocka);
            if(novaPozicija==0) return null;
            Figura mozdaPomeri=figure.Where(fig=>fig.index==novaPozicija&&fig.boja!=figura.boja).FirstOrDefault();
            Figura tekuca=figure.Where(fig=>fig.index==figura.index&&fig.boja==figura.boja).FirstOrDefault();
            if(tekuca==null) return null;
            potezi.Add(new Tuple<int, int>(tekuca.index,novaPozicija));
            tekuca.index=novaPozicija;
            
            if(mozdaPomeri==null) return potezi;
            potezi.Add(new Tuple<int, int>(mozdaPomeri.index,vratiUHome(mozdaPomeri)));
            mozdaPomeri.index=vratiUHome(mozdaPomeri);
            return potezi;



       }
       public int vratiUHome(Figura figura)
       {
           MyRange opsegBaze=(MyRange)typeof(Igra).GetField(figura.boja.ToString()+"Out").GetValue(null);
           foreach(int i in opsegBaze.getValues())
           {
               if(figure.Where(fig =>fig.index==i).FirstOrDefault()==null)
               return i;
           }
           return 0;

       }


       
         public Igra()
        {
            
        }
        public Igra(Korisnik igruKreirao)
        {
            this.groupNameGUID=Guid.NewGuid().ToString();
            this.aleaIactaEst=false;
            this.kreatorIgre=igruKreirao;
            this.status=statusIgre.cekanjeIgraca;
            figure=new List<Figura>(16);
            for(int i=1;i<=16;i=i+4)
            {
                
                figure.Add(new Figura(-(i),(Boja)(i/4),this));
                figure.Add(new Figura(-(i+1),(Boja)(i/4),this));
                figure.Add(new Figura(-(i+2),(Boja)(i/4),this));
                figure.Add(new Figura(-(i+3),(Boja)(i/4),this));
            }
            string shortID=Nanoid.Nanoid.Generate("123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ",7);
            this.accessCode=shortID;
            this.naPotezu=redosledPoteza.First.Value;
        }
        public bool slobodnaBoja(Boja boja)
        {
            string username=(string)this.GetType().GetProperty(boja.ToString()+"Username").GetValue(this);
            int id=(int)this.GetType().GetProperty(boja.ToString()+"IgracId").GetValue(this);
            if(username==null&&id==0) return true;
            return false;       
        }
        public List<string> zauzeteBoje()
        {
            List<string> boje=new List<string>();
            foreach(Boja b in Enum.GetValues(typeof (Boja)))
            { 
               if(!slobodnaBoja(b))
               boje.Add(b.ToString());
            }
            return boje;
        }
        public void dodajIgraca(Boja boja, Korisnik dodaj)
        {
            this.GetType().GetProperty(boja.ToString()+"IgracId").SetValue(this,dodaj.ID);
            this.GetType().GetProperty(boja.ToString()+"Username").SetValue(this,dodaj.username);
        }
        public List<string> vratiIgrace()
        {     
             List<string> lista=new List<string>();
            foreach(Boja b in Enum.GetValues(typeof (Boja)))
            { 
                string username=(string)this.GetType().GetProperty(b.ToString()+"Username").GetValue(this);
                
                 lista.Add(username);
            }
            return lista;
        }
        public bool sviPrisutni()
        {
             foreach(Boja b in Enum.GetValues(typeof (Boja)))
            { 
                if(slobodnaBoja(b))
                return false;
            }
            return true;

        }
        public Boja? kraj()
        {   
           foreach(Boja b in Enum.GetValues(typeof (Boja)))
            {  
                MyRange bojaHome=(MyRange)typeof(Igra).GetField(b.ToString()+"Home").GetValue(null);
                if(this.figure.Where(fig =>bojaHome.isWithin(fig.index)).ToList().Count()==4)
                return b;
            }
            return null;
        }











        
       /* public static Tuple<int,string,int,int> proveraKretanja(Figura figura, int kocka)
       {    
          
           if(figura.index>0)
           {
               int novaPozicija=(figura.index+kocka<57)?figura.index+kocka:(figura.index+kocka)%56;
            
            foreach(Boja b in Enum.GetValues(typeof (Boja)))
            {      
                if(b==figura.boja) continue;
                   
                MyRange bojaHome=(MyRange)typeof(Igra).GetField(b.ToString()+"Home").GetValue(null);

                if(bojaHome.isWithin(novaPozicija)||(figura.index==bojaHome.Start-2&&kocka==6)||(figura.index==bojaHome.Start-1&&kocka==6)||(figura.index==bojaHome.Start-1&&kocka==5))
                {   
                    novaPozicija+=4;
                    novaPozicija=(novaPozicija<57)?novaPozicija:(novaPozicija)%56;
                }
                 
            }
            if(novaPozicija>=(int)figura.boja*mbr+1&&figura.index<=((MyRange)typeof(Igra).GetField(figura.boja.ToString()+"Home").GetValue(null)).End&&figura.index>=((MyRange)typeof(Igra).GetField(figura.boja.ToString()+"Home").GetValue(null)).End-8)
                return new Tuple<int,string,int, int>(figura.index,figura.boja.ToString(),kocka,0);
            return new Tuple<int,string,int, int>(figura.index,figura.boja.ToString(),kocka,novaPozicija);
           } 
           return new Tuple<int,string,int, int>(figura.index,figura.boja.ToString(),kocka,0);
       }*/
    }
}