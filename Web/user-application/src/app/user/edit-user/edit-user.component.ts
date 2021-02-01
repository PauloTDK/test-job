import { Component, OnInit , Inject} from '@angular/core';
import {Router} from '@angular/router';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {first} from 'rxjs/operators';
import {User} from '../../model/user.model';
import {ApiService} from '../../service/api.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {

  dateOfBirth: Date;
  editForm: FormGroup;

  escolaridades: any = [
    {id: 1, text: 'Infantil'},
    {id: 2, text: 'Fundamental'},
    {id: 3, text: 'Médio'},
    {id: 4, text: 'Superior'},
  ];

  constructor(private formBuilder: FormBuilder, private router: Router, private apiService: ApiService) { }

  ngOnInit() {
    const userId = window.localStorage.getItem('editUserId');
    if (!userId) {
      alert('Invalid action.');
      this.router.navigate(['list-user']);
      return;
    }
    this.editForm = this.formBuilder.group({
      id: [''],
      nome: ['', Validators.required],
      sobreNome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      dataNascimento: ['', Validators.required],
      escolaridade: ['', [Validators.required, Validators.min(1) ]]
    });
    this.apiService.getUserById(+userId)
      .subscribe( data => {
        this.dateOfBirth = new Date(data.dataNascimento);
        this.editForm.setValue(data);
      });
  }

  onSubmit() {
    const item = this.editForm.value;
    item.escolaridade = Number(item.escolaridade);
    this.apiService.updateUser(item)
      .pipe(first())
      .subscribe(
        data => {
          console.log(data);
          if (data === 1) {
            alert('Usuário atualizado.');
            this.router.navigate(['list-user']);
          } else {
            alert(data);
          }
        },
        error => {
          alert(error.error);
        });
  }

}
