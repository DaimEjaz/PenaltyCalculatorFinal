import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Country } from 'src/app/models/Country';
import { ApiServiceService } from 'src/app/services/api-service.service';
import { Payload } from 'src/app/models/Payload';
import { Penalty } from 'src/app/models/Penalty';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';


@Component({
  selector: 'app-reactive-form',
  templateUrl: './reactive-form.component.html',
  styleUrls: ['./reactive-form.component.css']
})
export class ReactiveFormComponent implements OnInit {

  //Variable for saving the fetched country list
 countries: Country[];

 //Variables to be passed to display component to determine its state
 returnedPenalty: Penalty;
 shouldDisplay: boolean;
 gotError : boolean;

 //Name of the form
 reactiveForm !: FormGroup;

 //Variables for storing data, to be passed to service call
 query : Payload;
 currentCountry : Country;

 //Dependency injections
 constructor(private fb : FormBuilder, private service: ApiServiceService) { }

 ngOnInit(): void {

  //************ Getting the list of countries from API **************************//
  this.service.getAllCountries().subscribe(data => this.countries = data);


   //Schema for form data, with "Required" validations & initial values

   this.reactiveForm = this.fb.group({
    checkoutDate : [null, [
       Validators.required,
     ]],
     returnedDate : [null, [
       Validators.required,

     ]],
     country : [null, [
       Validators.required,
     ]],
   })

   

 }

 //Upon selecting a country value, this method saves it on the component
 selectedCountry(){
  this.currentCountry = this.reactiveForm.get("country").value;
  console.log(this.currentCountry);
 }

 //Submit method
 submit(){
 

  //Error-Handling via Try-Catch
  try {

    console.log(this.currentCountry);

    //If the form values are valid, then proceed
    if(this.reactiveForm.valid && this.reactiveForm.value.country != null ){

      //Setting state of display component,toggling the previous result(if any) off before displaying new result
      this.shouldDisplay = false;

      const formValues = this.reactiveForm.value;

      //Making the Payload object to be passed to the service
      this.query = {
        checkoutDate : formValues.checkoutDate,
        returnedDate : formValues.returnedDate,
        countryId : this.currentCountry.countryId,
      }

      if(this.query.checkoutDate > this.query.returnedDate){
        alert("Invalid date input")
        return
      }
      console.log(this.currentCountry.countryId);
      //Calling the service & saving the data into variable to be passed to Display component
      this.service.calculatePenalty(this.query).subscribe(data => this.returnedPenalty = data);

      //Setting state of display component
      this.shouldDisplay = true;
  
    }

    if(this.reactiveForm.value.country == null ){
      alert("Select a valid country");
      return
    }
  }
    
   catch (error) {
    //Setting state of Display component for Error message
    this.gotError = true;
    
  }
  
  
  }}
  