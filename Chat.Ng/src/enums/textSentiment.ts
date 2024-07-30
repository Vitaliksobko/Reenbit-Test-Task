export enum TextSentiment {
    Positive,
    Neutral,
    Negative
}


export function sentimentToString(sentiment: TextSentiment){
    switch(sentiment){
        case TextSentiment.Positive:
            return "Positive";
        case TextSentiment.Neutral:
            return "Neutral";
        case TextSentiment.Negative:
            return "Negative";
        default:
            return "Neutral";
    }
}