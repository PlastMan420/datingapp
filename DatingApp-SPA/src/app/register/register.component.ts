import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { AlertifyService } from '../services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter(); // output properties emmit events.
  model: any = {};

  constructor(private authService: AuthService, private alertify: AlertifyService) {

   }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      // subscribing to the returned observable returned by register method in auth.service.ts
      this.alertify.message('registeration successful'); // placeholder action
    }, error => {
      this.alertify.message('error'); // placeholder action
    });
  }
 cancel() {
  this.cancelRegister.emit(false); // we want to emit a 'false'
 }
}
