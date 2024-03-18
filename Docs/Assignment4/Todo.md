# In case there are some things we need to remember going forward.

## Domain Model

- [x] Add "URL" to the Guest entity as a value object
- [x] Add InvitationId as a value object
- [x] Add RequestId as a value object
- [x] Add UpdateDateRange method on Event
- [x] Go through domain model and make sure return types and parameters are the same

## General
- [ ] Remake UpdateDescription Tests to use the new systems we have created
- [ ] Make DateRange non-nullable and add default value
- [ ] Make DateRange not rely on DateTime.Now and implement ISystemTime or similar
- [ ] Change errorcode in EventErrors to be ascending
- [ ] An Email can be "Already in use", how should this be handled? When creating a Guest account
- [ ] Talk about folder structure, a participation is between an event and a guest, where should the participation be?
- [ ] Add Participation as an entity