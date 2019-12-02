import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';
// This component is a child component of member-list component
@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
@Input() user: User;
  constructor() { }

  ngOnInit() {
  }

}
