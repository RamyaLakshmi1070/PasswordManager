import { Component, OnInit } from '@angular/core';
import { PasswordService } from './services/password.service';
import { NotifierService } from 'angular-notifier';
import { Password } from './models/password.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  passwords: Password[] = [];
  editId: number | null = null;
  decryptedId: number | null = null;
  decryptedPassword: string = '';
  editedPassword: string = '';

  addMode: boolean = false;
  newPassword: any = {
    app: '',
    category: '',
    userName: '',
    plainPassword: ''
  };

  constructor(
    private passwordService: PasswordService,
    private toastr: NotifierService
  ) {}

  ngOnInit(): void {
    this.loadPasswords();
  }

  loadPasswords(): void {
    this.passwordService.getAll().subscribe({
      next: (res) => this.passwords = res,
      error: () => this.toastr.notify('error','Failed to load passwords')
    });
  }

 decrypt(item: Password): void {
  this.passwordService.getById(item.id).subscribe({
    next: (response) => {
      this.decryptedId = item.id;
      this.decryptedPassword = response.encryptedPassword; 
      this.toastr.notify('success', 'Password has been decrypted');
    },
    error: () => {
      this.toastr.notify('error', 'Failed to decrypt password');
    }
  });
}

  edit(item: Password): void {
    this.editId = item.id;
    this.decryptedId = null;
    this.editedPassword = item.encryptedPassword;
  }

  cancelEdit(): void {
    this.editId = null;
    this.editedPassword = '';
  }

  save(item: Password): void {
    if (!item.app || !item.category || !item.userName || !this.editedPassword) {
      this.toastr.notify('warning','All fields are required');
      return;
    }

    const updated = {
      ...item,
      encryptedPassword: this.editedPassword
    };

    this.passwordService.update(item.id, updated).subscribe({
      next: () => {
        this.toastr.notify('success','Password updated successfully');
        this.editId = null;
        this.loadPasswords();
      },
      error: () => this.toastr.notify('error','Failed to update password')
    });
  }

  delete(item: Password): void {
    if (confirm('Are you sure you want to delete this password?')) {
      this.passwordService.delete(item.id).subscribe({
        next: () => {
          this.toastr.notify('success','Password deleted');
          this.loadPasswords();
        },
        error: () => this.toastr.notify('error','Failed to delete password')
      });
    }
  }

  addNewRow(): void {
    this.addMode = true;
    this.newPassword = {
      app: '',
      category: '',
      userName: '',
      plainPassword: ''
    };
  }

  cancelAdd(): void {
    this.addMode = false;
    this.newPassword = {};
  }

  saveNewPassword(): void {
    const { app, category, userName, plainPassword } = this.newPassword;
    if (!app || !category || !userName || !plainPassword) {
      this.toastr.notify('warning','All fields are required');
      return;
    }

    const newItem: Password = {
      id: 0,
      app,
      category,
      userName,
      encryptedPassword: plainPassword
    };

    this.passwordService.add(newItem).subscribe({
      next: () => {
        this.toastr.notify('success','Password added');
        this.addMode = false;
        this.loadPasswords();
      },
      error: () => this.toastr.notify('error','Failed to add password')
    });
  }
}
