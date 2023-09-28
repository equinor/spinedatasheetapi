# Changelog

## [1.8.0](https://github.com/equinor/spinedatasheetapi/compare/v1.7.0...v1.8.0) (2023-09-28)


### Features

* Add endpoint and service for user lookup ([#93](https://github.com/equinor/spinedatasheetapi/issues/93)) ([adff941](https://github.com/equinor/spinedatasheetapi/commit/adff94173e3532ac0f4fb13d1eb045d17accddfd))
* Add max length to comment text ([#87](https://github.com/equinor/spinedatasheetapi/issues/87)) ([efaac84](https://github.com/equinor/spinedatasheetapi/commit/efaac8457d475215621c12a8922652f4bcab9235))
* Add more TR3111 properties to tag data ([564e46a](https://github.com/equinor/spinedatasheetapi/commit/564e46a581627a4457209943b1279c21a040a2cc))
* Display message deleted ([#90](https://github.com/equinor/spinedatasheetapi/issues/90)) ([dafa491](https://github.com/equinor/spinedatasheetapi/commit/dafa491cf07471a58745441ea641a4484561ac14))
* refactoring dto's enum ([#105](https://github.com/equinor/spinedatasheetapi/issues/105)) ([d79ded0](https://github.com/equinor/spinedatasheetapi/commit/d79ded09b02174d38410c309839c8da36dfa86b4))
* Refactoring of tagdata & review collections ([#103](https://github.com/equinor/spinedatasheetapi/issues/103)) ([f4e4441](https://github.com/equinor/spinedatasheetapi/commit/f4e4441d7763fc46330f9b37e104e3b0bd511c65))
* Refactoring the other collections ([#102](https://github.com/equinor/spinedatasheetapi/issues/102)) ([b9f1006](https://github.com/equinor/spinedatasheetapi/commit/b9f1006e379562cdef2c8124fd4ec298883e6626))
* replacing comment with the conversation model ([#92](https://github.com/equinor/spinedatasheetapi/issues/92)) ([42e7f5d](https://github.com/equinor/spinedatasheetapi/commit/42e7f5d09cfe3ad3de9789ad50e1924d26a6aa50))
* Updated instrument models ([11cd752](https://github.com/equinor/spinedatasheetapi/commit/11cd752ad5e516bf8a4e99bdb05b9bdb33bea6c3))

## [1.7.0](https://github.com/equinor/spinedatasheetapi/compare/v1.6.0...v1.7.0) (2023-08-18)


### Features

* Add cache for azure id ([#75](https://github.com/equinor/spinedatasheetapi/issues/75)) ([7709b12](https://github.com/equinor/spinedatasheetapi/commit/7709b128417a5bf2e8460e9db9fbafc942255e94))
* Add comment model and endpoint ([#69](https://github.com/equinor/spinedatasheetapi/issues/69)) ([bc572ae](https://github.com/equinor/spinedatasheetapi/commit/bc572ae1ccda896cacb7a732b69478cd4a3d53f2))
* Add EF ([#82](https://github.com/equinor/spinedatasheetapi/issues/82)) ([44247ce](https://github.com/equinor/spinedatasheetapi/commit/44247cebf6bc4d83d28c8634a9e1a0cf339b4dfa))
* Add EF repositories ([#85](https://github.com/equinor/spinedatasheetapi/issues/85)) ([047f505](https://github.com/equinor/spinedatasheetapi/commit/047f505f47508bdbc025b9e8f759a35fc583329b))
* Add review model ([#77](https://github.com/equinor/spinedatasheetapi/issues/77)) ([d35f8b1](https://github.com/equinor/spinedatasheetapi/commit/d35f8b1cf0d0c838728d4b00ae52dfd443cb3f2b))
* Added NORSOK instrument properties and new NORSOK dummy data ([#79](https://github.com/equinor/spinedatasheetapi/issues/79)) ([453f22f](https://github.com/equinor/spinedatasheetapi/commit/453f22f9f8322be2b56b876e7a6ee18e1d6f6583))
* Added TR3111 properties to Tag Comparison ([5529b47](https://github.com/equinor/spinedatasheetapi/commit/5529b47dc6f81c3f397260e5a5540ffee6ad90b2))
* deleting comments ([#81](https://github.com/equinor/spinedatasheetapi/issues/81)) ([a6f0e0c](https://github.com/equinor/spinedatasheetapi/commit/a6f0e0cc0e5d43f53570c37ae1a35399a1e48d97))
* implemented edit for comments ([#84](https://github.com/equinor/spinedatasheetapi/issues/84)) ([77b6b92](https://github.com/equinor/spinedatasheetapi/commit/77b6b92dea70d29c881bf7943ba81c7c983f7fd6))
* Renamed purchaserReq and supplierOff to Instrument ([25aa44e](https://github.com/equinor/spinedatasheetapi/commit/25aa44ed13caf3d8e1c3a4fa2ba5dc904001b898))
* set "last edited" on edited comments ([#86](https://github.com/equinor/spinedatasheetapi/issues/86)) ([672dbff](https://github.com/equinor/spinedatasheetapi/commit/672dbff32079a836821cd4c346469bcf672dee1d))


### Bug Fixes

* **pipeline:** update pipeline to use reusable components ([9f1f0d6](https://github.com/equinor/spinedatasheetapi/commit/9f1f0d608cf35eebc143d646ae5bd895883774fb))
* remove other dockermethod ([b4e784c](https://github.com/equinor/spinedatasheetapi/commit/b4e784c57206c8f69e926e3372e7cf3d1d7b35b9))


### For developers

* Change tagdata model structure ([#76](https://github.com/equinor/spinedatasheetapi/issues/76)) ([9f09eeb](https://github.com/equinor/spinedatasheetapi/commit/9f09eeb0d377f0a99c2610a390065a71b9c42eed))

## [1.6.0](https://github.com/equinor/spinedatasheetapi/compare/v1.5.0...v1.6.0) (2023-06-01)


### Features

* Added JIP33 Electrical to backend ([6b3d6b1](https://github.com/equinor/spinedatasheetapi/commit/6b3d6b1fbcb7d93fd8661477736bd90c88368ad0))
* Added JIP33 Mechanical with dummy data ([5ba7aaa](https://github.com/equinor/spinedatasheetapi/commit/5ba7aaa6bb7435eaa9933e5a692398ad478eeec7))
* Token auth in backend AB#{108506} ([52281b3](https://github.com/equinor/spinedatasheetapi/commit/52281b3902df1d139fb1d148b685566b22e32436))

## [1.5.0](https://github.com/equinor/spinedatasheetapi/compare/v1.4.0...v1.5.0) (2023-04-18)


### Features

* Add auth config ([#62](https://github.com/equinor/spinedatasheetapi/issues/62)) ([85ae41e](https://github.com/equinor/spinedatasheetapi/commit/85ae41e24748ebcb8d53563a6731003ab9d798dc))


### Bug Fixes

* Update duplicate GUIDs on dummy data ([#65](https://github.com/equinor/spinedatasheetapi/issues/65)) ([bb9e075](https://github.com/equinor/spinedatasheetapi/commit/bb9e0759e283786a82c1760809ca395096985493))

## [1.4.0](https://github.com/equinor/spinedatasheetapi/compare/v1.3.0...v1.4.0) (2023-04-12)


### Features

* Add more dummy data ([#60](https://github.com/equinor/spinedatasheetapi/issues/60)) ([15bb78f](https://github.com/equinor/spinedatasheetapi/commit/15bb78fd91594faec70ab7cad4ee473e87d6e0f1))
* adding two more set of dummy data ([15bb78f](https://github.com/equinor/spinedatasheetapi/commit/15bb78fd91594faec70ab7cad4ee473e87d6e0f1))

## [1.3.0](https://github.com/equinor/spinedatasheetapi/compare/v1.2.0...v1.3.0) (2023-04-12)


### Features

* More dummy data examples ([#59](https://github.com/equinor/spinedatasheetapi/issues/59)) ([a368f84](https://github.com/equinor/spinedatasheetapi/commit/a368f84965c824144df93a4aa9b403ce46b62089))


### Bug Fixes

* added appsettings in runtime ([#56](https://github.com/equinor/spinedatasheetapi/issues/56)) ([c303c89](https://github.com/equinor/spinedatasheetapi/commit/c303c896cf85848d9c7b2676d4d2b8376e46a5c1))


### For developers

* Dummy data ([#53](https://github.com/equinor/spinedatasheetapi/issues/53)) ([e8a96ce](https://github.com/equinor/spinedatasheetapi/commit/e8a96ce7c4f153277ae44896bec9a59438da2205))
* dummy data ([#55](https://github.com/equinor/spinedatasheetapi/issues/55)) ([d81577b](https://github.com/equinor/spinedatasheetapi/commit/d81577b387237197e16ed8e48cdbcfaa23e8e9f0))
* remove config logging ([eb457c5](https://github.com/equinor/spinedatasheetapi/commit/eb457c52e67948446c75ead006f72f3a3a128e2b))
* remove too much logging in config setup appconfig ([#57](https://github.com/equinor/spinedatasheetapi/issues/57)) ([eb457c5](https://github.com/equinor/spinedatasheetapi/commit/eb457c52e67948446c75ead006f72f3a3a128e2b))
* update dummy data ([e8a96ce](https://github.com/equinor/spinedatasheetapi/commit/e8a96ce7c4f153277ae44896bec9a59438da2205))

## [1.2.0](https://github.com/equinor/spinedatasheetapi/compare/v1.1.0...v1.2.0) (2023-03-29)


### Features

* Add missing properties to datsheet model ([#49](https://github.com/equinor/spinedatasheetapi/issues/49)) ([199d791](https://github.com/equinor/spinedatasheetapi/commit/199d7914914cd79751ab85784062a375f9c15ec7))


### For developers

* Update dummy data ([#51](https://github.com/equinor/spinedatasheetapi/issues/51)) ([1fcfd92](https://github.com/equinor/spinedatasheetapi/commit/1fcfd927fdf6876f56dfeefabc64e7c7ae0e917a))

## [1.1.0](https://github.com/equinor/spinedatasheetapi/compare/v1.0.0...v1.1.0) (2023-03-29)


### Features

* Add contract service and endpoint ([#46](https://github.com/equinor/spinedatasheetapi/issues/46)) ([5a42ee8](https://github.com/equinor/spinedatasheetapi/commit/5a42ee8cc6ac64e2b01b3f604547ea35a21b966a))
* Add CORS ([#45](https://github.com/equinor/spinedatasheetapi/issues/45)) ([8808f99](https://github.com/equinor/spinedatasheetapi/commit/8808f99e5bd1c65ea03e2dd632cff8cd7e123e0a))
* Add endpoint to get datasheet per project ([#48](https://github.com/equinor/spinedatasheetapi/issues/48)) ([5260dd2](https://github.com/equinor/spinedatasheetapi/commit/5260dd22fad69087750b53baf670542bc1e72946))
* **auth:** ✨ Add auth to backend AB#{102792} ([96c612e](https://github.com/equinor/spinedatasheetapi/commit/96c612e02dd867fc564516ad9a3185295309f816))
* **logging:** ✨ Add logging feature to backend. AB#{105475} ([33faed8](https://github.com/equinor/spinedatasheetapi/commit/33faed891b5ca997253feff1a075d4fec55fff6b))


### Bug Fixes

* **config:** 🐛 Nuget config missing ([560f15f](https://github.com/equinor/spinedatasheetapi/commit/560f15fc31d6c2b2bae28e4b0a1f562ab19ba4bf))
* Make datasheet properties nullable ([#47](https://github.com/equinor/spinedatasheetapi/issues/47)) ([6b65992](https://github.com/equinor/spinedatasheetapi/commit/6b659926e8db60abf3f25f80c8f33c5287d6ed3e))
* **pipeline:** 🐛 Add dependabot yml to repo ([dc7f460](https://github.com/equinor/spinedatasheetapi/commit/dc7f460ef65c87e980b8873524c2ed04661829c5))
* **pipelines:** 🐛 Changes in snyk auth ([4bdcfe0](https://github.com/equinor/spinedatasheetapi/commit/4bdcfe0251c32d20db0634b172ace346b971d1c4))
* **pipelines:** 🐛 Error in PR Name Validator ([bf873a2](https://github.com/equinor/spinedatasheetapi/commit/bf873a252836f6996fe16a89826c726ee7ff52ce))
* **pipelines:** 🐛 Error in Superlint job in Code Quality ([82e7a82](https://github.com/equinor/spinedatasheetapi/commit/82e7a82b763a0f566e8f5206746307117a24051a))
* **pipelines:** 🐛 Linting errors in yml ([8d37897](https://github.com/equinor/spinedatasheetapi/commit/8d3789738faa7748906647d7c19c98557e775a8a))


### For developers

* 📚 Some duplicate and incorrect data in docs ([1b1dded](https://github.com/equinor/spinedatasheetapi/commit/1b1dded589ffb260c9f8cadf3447b3f436400ab4))
* Add some structure and content to README ([170a098](https://github.com/equinor/spinedatasheetapi/commit/170a0983740f41c5bab711cb859b2dadde783dbb))
* **deps:** bump actions/checkout from 2 to 3 ([1c22659](https://github.com/equinor/spinedatasheetapi/commit/1c22659cd2e3bfd911dad06bce6607399d45fc74))
* **deps:** bump docker/login-action from 1 to 2 ([ccbda83](https://github.com/equinor/spinedatasheetapi/commit/ccbda8363888a67c3aa70c351294eda27bb2856a))

## 1.0.0 (2023-03-20)


### Features

* add changelog file to repo ([0b03f69](https://github.com/equinor/spinedatasheetapi/commit/0b03f698f92bffe32221f6bf97f56b1413a0a209))
* Add tags to container AB{103950} ([e3d59ee](https://github.com/equinor/spinedatasheetapi/commit/e3d59ee75ec1a44680cc7ba88a1512e428fdd8dc))
* Pipeline steps to trigger radix AB#{103818} ([c7276af](https://github.com/equinor/spinedatasheetapi/commit/c7276af91a6733c7ad8572a7d8ef910d766cbb76))


### Bug Fixes

* azure auth with service principal AB#{103818} ([d1078df](https://github.com/equinor/spinedatasheetapi/commit/d1078dfff3e792a8373c9e44324c478ef13be31d))
* even more errors in indentation ([3911ece](https://github.com/equinor/spinedatasheetapi/commit/3911ece2f6f3acac0d829cb6ec93056b67117486))
* update release please config ([84fa7f8](https://github.com/equinor/spinedatasheetapi/commit/84fa7f83d18f80a1ab0433fd593627c07e6be9f2))
* wrong indentation in yml ([cdabd94](https://github.com/equinor/spinedatasheetapi/commit/cdabd946975f8df8b4ce255ceb908cd4fe2eda52))
