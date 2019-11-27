import { Injectable } from '@angular/core';
declare let alertify: any; // declare alertify at the top of the file in this service so that it doesn't throw errors

// 'declare' lets you use non TS stuff inside TS

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  constructor() { }
  confirm(message: string, oKCallback: () => any) {
    // message is the displayed message. oKcallback of type any takes the user confirmation and executes a function
    // "Are you sure you wanna do this?" --> "Okay"
    alertify.confirm(message, (e: any) => {
      if (e) {
        oKCallback();
      } else {}
    }); // alertify.confirm()
  }
  success(message: string) {
    alertify.success(message);
  }

  error(message: string) {
    alertify.error(message);
  }

  warning(message: string) {
    alertify.warning(message);
  }

  message(message: string) {
    alertify.message(message);
  }
}
