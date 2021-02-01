import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, ValidationErrors } from '@angular/forms';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import { Escolaridade } from 'src/app/model/user.model';
import {ApiService} from '../../service/api.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {
  constructor(private formBuilder: FormBuilder, private router: Router, private apiService: ApiService) { }

  addForm: FormGroup;

  escolaridades: any = [
    {id: 1, text: 'Infantil'},
    {id: 2, text: 'Fundamental'},
    {id: 3, text: 'Médio'},
    {id: 4, text: 'Superior'},
  ];

  ngOnInit() {
    this.addForm = this.formBuilder.group({
      id: [],
      nome: ['', Validators.required],
      sobreNome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      dataNascimento: ['', Validators.required],
      escolaridade: ['', [Validators.required, Validators.min(1) ]]
    });

  }

  onSubmit() {
    const item = this.addForm.value;
    item.escolaridade = Number(item.escolaridade);

    if (!this.addForm.valid) {
      return;
    }

    this.apiService.createUser(this.addForm.value)
      .subscribe(
        data => {
          if (data === 1) {
            this.router.navigate(['list-user']);
          }
        },
        error => {
          alert(error.error);
        }
      );
  }

  getAllErrors(form: FormGroup | FormArray): { [key: string]: any; } | null {
    let hasError = false;
    const result = Object.keys(form.controls).reduce((acc, key) => {
        const control = form.get(key);
        const errors = (control instanceof FormGroup || control instanceof FormArray)
            ? this.getAllErrors(control)
            : control.errors;
        if (errors) {
            acc[key] = errors;
            hasError = true;
        }
        return acc;
    }, {} as { [key: string]: any; });
    return hasError ? result : null;
}

}
