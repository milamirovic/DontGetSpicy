import { Boja } from "./Consts";
import figura from "./figura";
import Polje from "./polje";

export default class Igra{

    constructor(player)
    {

        this.outKoor=[
            { "x":"25", "y":"25" },
            { "x":"75", "y":"25" },
            { "x":"25", "y":"75" },
            { "x":"75", "y":"75" },
            
            { "x":"475", "y":"25" },
            { "x":"525", "y":"25" },
            { "x":"475", "y":"75" },
            { "x":"525", "y":"75" },
            { "x":"475", "y":"475" },
            { "x":"525", "y":"475" },
            { "x":"475", "y":"525" },
            { "x":"525", "y":"525" },
            
            { "x":"25", "y":"475" },
            { "x":"75", "y":"475" },
            { "x":"25", "y":"525" },
            { "x":"75", "y":"525" }
            
            ]

        this.inKoors=[
            { "x":"25", "y":"225" },
            { "x":"75", "y":"225" },
            { "x":"125", "y":"225" },
            { "x":"175", "y":"225" },
            { "x":"225", "y":"225" },
            
            { "x":"225", "y":"175" },
            { "x":"225", "y":"125" },
            { "x":"225", "y":"75" },
            { "x":"225", "y":"25" },
            { "x":"275", "y":"25" },

            
            { "x":"275", "y":"75" },
            { "x":"275", "y":"125" },
            { "x":"275", "y":"175" },
            { "x":"275", "y":"225" },
            
            { "x":"325", "y":"25" },
            { "x":"325", "y":"75" },
            { "x":"325", "y":"125" },
            { "x":"325", "y":"175" },
            { "x":"325", "y":"225" },
            
            { "x":"375", "y":"225" },
            { "x":"425", "y":"225" },
            { "x":"475", "y":"225" },
            { "x":"525", "y":"225" },
            { "x":"525", "y":"275" },


            
            { "x":"475", "y":"275" },
            { "x":"425", "y":"275" },
            { "x":"375", "y":"275" },
            { "x":"325", "y":"275" },


            
            { "x":"525", "y":"325" },
            { "x":"475", "y":"325" },
            { "x":"425", "y":"325" },
            { "x":"375", "y":"325" },
            { "x":"325", "y":"325" },
            
            { "x":"325", "y":"375" },
            { "x":"325", "y":"425" },
            { "x":"325", "y":"475" },
            { "x":"325", "y":"525" },
            { "x":"275", "y":"525" },


           
            { "x":"275", "y":"475" },
            { "x":"275", "y":"425" },
            { "x":"275", "y":"375" },
            { "x":"275", "y":"325" },


            { "x":"225", "y":"525" },
            { "x":"225", "y":"475" },
            { "x":"225", "y":"425" },
            { "x":"225", "y":"375" },
            { "x":"225", "y":"325" },
            
            { "x":"175", "y":"325" },
            { "x":"125", "y":"325" },
            { "x":"75", "y":"325" },
            { "x":"25", "y":"325" },
            { "x":"25", "y":"275" },

            
            { "x":"75", "y":"275" },
            { "x":"125", "y":"275" },
            { "x":"175", "y":"275" },
            { "x":"225", "y":"275" }
        ]
        
        this.kreatorIgre
        this.crveniIgrac;
        this.zeleniIgrac;
        this.plaviIgrac;
        this.zutiIgrac;
        this.naPotezu;
        this.player=player
        this.glavnaPolja=new Array();
        this.outPolja=new Array();
        this.mbr=14;
        for (let i = 1; i <= 56; i++) {
           this.glavnaPolja.push(new Polje(i,null,this.inKoors[i-1]));           
        }
        for (let i = 0; i < 16; i++) {
            this.outPolja.push(new Polje((i+1)*(-1),new figura(Boja.naziv[Math.floor(i/4)]), this.outKoor[i]));           
         }
         
         
    }


    odigrajPotez(potez1, potez2)
    {
           this.pomeriFiguru(potez1.staraPozicija,potez1.novaPozicija);
           if(potez2!=null)
           this.pomeriFiguru(potez2.staraPozicija,potez2.novaPozicija);

    }
    pomeriFiguru(staraPozicija,novaPozicija)
    {   
        let traziStara=(staraPozicija>0)?this.glavnaPolja:this.outPolja;
        let traziNova=(novaPozicija>0)?this.glavnaPolja:this.outPolja;
       
        let staroPolje=traziStara.find(polje=>polje.index==staraPozicija&&polje.figura!=null);
        if(staroPolje==null) console.log("error");
           
        let novoPolje=traziNova.find(polje=>polje.index==novaPozicija);
        if(novoPolje==null) console.log("error");
        novoPolje.figura=staroPolje.figura;
        staroPolje.figura=null;

    }
}