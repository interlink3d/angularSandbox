import { Component, OnInit } from '@angular/core';
import { SharedService } from "src/app/shared.service";

@Component({
  selector: 'app-show-dep',
  templateUrl: './show-dep.component.html',
  styleUrls: ['./show-dep.component.css']
})
export class ShowDepComponent implements OnInit {

  constructor(private service: SharedService) { }

  DepartmentList:any = [];

  // first thing called when this class is in scope
  // calling the method sothat it pulls back API response values
  // subscribe method is possible because we declared it as an observable return type
  ngOnInit(): void {
    this.refreshDeptList();
  }

  refreshDeptList() {
    this.service.getDepList().subscribe(data=> {
      this.DepartmentList = data;
    });
  }



}
