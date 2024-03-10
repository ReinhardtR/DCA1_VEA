# In case there are some things we need to remember going forward.

## Domain Model

- [ ] Add "URL" to the Guest entity as a value object
- [ ] Add InvitationId as a value object
- [ ] Add RequestId as a value object
- [x] Add UpdateDateRange method on Event
- [ ] Go through domain model and make sure return types and parameters are the same

## General
- [ ] Make DateRange non-nullable and add default value
- [ ] Make DateRange not rely on DateTime.Now and implement ISystemTime or similar
- [ ] Change errorcode in EventErrors to be ascending
- [ ] An Email can be "Already in use", how should this be handled? When creating a Guest account
- [ ] Talk about folder structure, a participation is between an event and a guest, where should the participation be?