export enum Status {
    Open,
    Completed
  }
  
  export interface Todo {
    id: number;
    description: string;
    status: Status;
  }
  
  export interface HttpResponse {
    response: any;
    statusCode: number;
  }
