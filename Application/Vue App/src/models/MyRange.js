export default class MyRange{

    constructor(start,end)
    {
        this.Start=(start>end)?end:start;
        this.End=(start>end)?start:end;
    }
    isWithin(questionable)
    {
        return questionable>=this.Start&&questionable<=this.End;
    }
    
}