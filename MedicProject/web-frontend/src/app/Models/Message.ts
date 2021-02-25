export class Message{
  id?: number;
  transmitterId?: number;
  transmitterEmail?: string;
  transmitterFirstName?: string;
  receiverId?: number;
  receiverEmail?: string;
  receiverFirstName?: string;
  content?: string;
  dateRead?: Date;
  dateSent?: Date;
}

