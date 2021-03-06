import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {

  canDeactivate(component: MemberEditComponent): boolean {
    if (component.profileForm.dirty) {
      return confirm("Any unsaved changes will be lost. Are you sure you want to leave?")
    }
    return true
  }

}

