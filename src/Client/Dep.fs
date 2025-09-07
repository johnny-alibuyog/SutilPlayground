namespace SutilPlayground.Client

// module Dep =

//     type NotificationType =
//         | Email of address: string
//         | Sms of phone: string

//     type UserSettings = { notificationType: NotificationType }

//     module DatabaseService =
//         let getUserSettings userId = async {
//             return {
//                 notificationType = Email(address = "john@g.com")
//             }
//         }

//     module EmailService =
//         let sendEmail address message = async {
//             printfn "Sending email to %s: %s" address message
//         }

//         let sendSms phone message = async {
//             printfn "Sending sms to %s: %s" phone message
//         }


//     let notifyUser userId message = task {
//         let! userSettings = DatabaseService.getUserSettings userId

//         match userSettings.notificationType with
//         | Email address ->
//             return! EmailService.sendEmail address message
//         | Sms phone ->
//             return! EmailService.sendSms phone message
//     }

//     // let notifyUser' env userId message = task {
//     //     let! userSettings = env.getUserSettings userId

//     //     match userSettings.notificationType with
//     //     | Email address ->
//     //         return! env.sendEmail address message
//     //     | Sms phone ->
//     //         return! env.sendSms phone message
//     // }