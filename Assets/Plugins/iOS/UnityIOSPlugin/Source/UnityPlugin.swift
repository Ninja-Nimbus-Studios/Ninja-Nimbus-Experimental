//
//  UnityPlugin.swift
//  UnityiOSPlugin
//
//  Created by Tatsuo Kumamoto on 7/9/24.
//

import Foundation
import CoreFoundation
import CoreHaptics

@objc public class UnityPlugin : NSObject {
    
    @objc public static let shared = UnityPlugin()
    @objc public func AddTwoNumber(a:Int,b:Int ) -> Int {
        
        let result = a+b;
        return result;
    }
}

@available(iOS 13.0, *)
@objc public class UnityVibrationPlugin : NSObject {
    @objc public static let shared = UnityVibrationPlugin()
    //vibration motor control
    @objc private var engine: CHHapticEngine?
    
    @objc public func PrepareVibration(){
            guard CHHapticEngine.capabilitiesForHardware().supportsHaptics else {return}
            do{
                engine=try CHHapticEngine()
                try engine?.start()
            }catch{
                print("Problems starting haptic engine: \(error.localizedDescription)")
            }
        }
        
    @objc public func VibratePhone(seconds: Double, isVibrating:Bool){
        guard CHHapticEngine.capabilitiesForHardware().supportsHaptics else {return}
        PrepareVibration()
        if (isVibrating){
            var events=[CHHapticEvent]()
            let intensity = CHHapticEventParameter(parameterID: .hapticIntensity, value: 1)
            let sharpness = CHHapticEventParameter(parameterID: .hapticSharpness, value: 1)
            let event = CHHapticEvent(eventType: .hapticContinuous, parameters: [intensity, sharpness], relativeTime: 0, duration: seconds)
            print("Current haptic event: \(event.eventParameters), \(event.duration)")
            events.append(event)

            do {
                let pattern = try CHHapticPattern(events: events, parameters: [])
                print("Pattern: \(pattern.description)")
                let player = try engine?.makePlayer(with: pattern)
                print("Player: \(String(describing: player))")
                print("Engine: \(String(describing: engine))")
                try player?.start(atTime:0)
            }catch{
                print("Failed to play pattern \(error.localizedDescription)")
            }
        }else{
            engine?.stop()
            
            PrepareVibration()
        }
        
    }
}
