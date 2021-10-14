import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.css']
})
export class ShowEmpComponent implements OnInit {

  constructor(private service: SharedService) { }

  EmployeeList:any = [];

  ModalTitle: string;
  ActivateAddEditEmpComp: boolean=false;
  emp: any;

  // first thing called when this class is in scope
  // calling the method sothat it pulls back API response values
  // subscribe method is possible because we declared it as an observable return type
  ngOnInit(): void {
    this.refreshEmpList();
  }

  addClick() {
    this.emp = {
      EmployeeId:0,
      EmployeeName:"",
      Dapartment:"",
      DateOfJoining:"",
      PhotoFileName:"anonymous.png"
    }
    this.ModalTitle="Add Employee";
    this.ActivateAddEditEmpComp=true;

  }

  editClick(item:any) {
    console.log(item);
    this.emp=item;
    this.ModalTitle="Edit Employee";
    this.ActivateAddEditEmpComp=true;
  }

  deleteClick(item:any){
    if(confirm('Are you sure??')){
      this.service.deleteEmployee(item.EmployeeId).subscribe(data=>{
        //once the async call returns the alert method will be called
        alert(data.toString());
        //lets refresh the list so we dont have to reload page
        this.refreshEmpList();
      })
    }
  }

  closeClick() {
    this.ActivateAddEditEmpComp=false;
    this.refreshEmpList();
  }

  refreshEmpList() {
    this.service.getEmpList().subscribe(data=> {
      this.EmployeeList = data;
    });
  }
}
