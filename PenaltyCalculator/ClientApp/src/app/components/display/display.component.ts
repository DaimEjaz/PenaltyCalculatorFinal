import { Component, Input, OnInit } from '@angular/core';
import { Penalty } from 'src/app/models/Penalty';

@Component({
  selector: 'app-display',
  templateUrl: './display.component.html',
  styleUrls: ['./display.component.css']
})
export class DisplayComponent implements OnInit {

  //Input variables, that determine the display content of this component.
  //To be initialized by Reactive-form component
  @Input() displayOn : boolean = false;
  @Input() penaltyObj ?: Penalty;
  @Input() isError : boolean = false;

  constructor() { }

  ngOnInit() {
  }

}