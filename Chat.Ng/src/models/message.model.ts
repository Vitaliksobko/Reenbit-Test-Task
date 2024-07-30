import { TextSentiment } from "../enums/textSentiment";
import { UserModel } from "./user.model";


export class MessageModel {
    id: string = "";
    text: string = "";
    date: string = "";
    userId: string = "";
    user: UserModel = new UserModel();
    sentiment: TextSentiment = TextSentiment.Neutral; 
}