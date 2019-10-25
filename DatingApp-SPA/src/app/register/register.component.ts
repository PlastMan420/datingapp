import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter(); // output properties emmit events.
  model: any = {};

  constructor(private authService: AuthService) {

   }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      // subscribing to the returned observable returned by register method in auth.service.ts
      console.log('registeration successful'); // placeholder action
    }, error => {
      console.log('error'); // placeholder action
    });
  }
 cancel() {
  this.cancelRegister.emit(false); // we want to emit a 'false'
  console.log('cancelled');
 }
}
